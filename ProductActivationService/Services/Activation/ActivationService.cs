using AutoMapper;
using ProductActivationService.Entities;
using ProductActivationService.Models;
using ProductActivationService.Repositories;
using static ProductActivationService.Services.IActivationService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProductActivationService.Services
{
    /// <summary>
    /// アクティベーション サービス
    /// </summary>
    public class ActivationService(ILogger<IActivationService> logger, ICustomerRepository customerRepository, ITokenRepository tokenRepository, IMapper mapper, IConfiguration configuration) : IActivationService
    {
        private ILogger Logger => logger;
        private IMapper Mapper => mapper;
        private IConfiguration Configuration => configuration;
        private ICustomerRepository CustomerRepository => customerRepository;
        private ITokenRepository TokenRepository => tokenRepository;

        /// <summary>
        /// 詳細取得
        /// </summary>
        /// <param name="postModel">ポストモデル</param>
        public async ValueTask<ActivationServiceResult> Activation(ActivationPostModel postModel)
        {
            Logger.LogInformation("Visited:Activation");
            // 顧客データを取得
            var customerEntity = await CustomerRepository.GetDetail(postModel.CustomerId);
            if (customerEntity == null)
            {
                Logger.LogInformation("顧客が存在しません(CustomerId: {CustomerId})", postModel.CustomerId);
                return new ActivationServiceResult(null, ServiceStatus.Unauthorized);
            }
            // プロダクトキーのチェック
            if (customerEntity.ProductKey != postModel.ProductKey)
            {
                Logger.LogInformation("プロダクトキーが一致しません(ProductKey: {ProductKey})", postModel.ProductKey);
                return new ActivationServiceResult(null, ServiceStatus.Unauthorized);
            }
            // トークンを生成
            var tokenModel = new TokenUpdateModel()
            {
                CustomerId = postModel.CustomerId,
                ClientGuid = postModel.ClientGuid,
                ClientHostName = postModel.ClientHostName,
            };
            var tokenEntity = Mapper.Map<TokenUpdateModel, TokenEntity>(tokenModel);
            await TokenRepository.Insert(tokenEntity);
            await TokenRepository.Save();
            var newTokenEntity = await TokenRepository.GetDetail(tokenEntity.Sub.ToString());
            if (newTokenEntity == null)
            {
                // トークンの生成に失敗
                Logger.LogInformation("トークンの生成に失敗しました");
                return new ActivationServiceResult(null, ServiceStatus.Unauthorized);
            }

            // JWT（アクセストークン）の作成
            var claims = new[] { new Claim("sub", newTokenEntity.Sub.ToString()) };
            var subject = new ClaimsIdentity(claims);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateJwtSecurityToken(subject: subject, signingCredentials: credentials);
            token.Payload.Remove("nbf");
            token.Payload.Remove("iat");
            token.Payload.Remove("exp");

            // アクティベーションモデルの作成
            var result = new ActivationModel()
            {
                AccessToken = handler.WriteToken(token),
                TokenType = "Bearer",
                Exp = newTokenEntity.Exp,
            };
            return new ActivationServiceResult(result, ServiceStatus.Ok);
        }
    }

}
