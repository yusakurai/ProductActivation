using ProductActivationService.Models;
using static ProductActivationService.Services.IAdminLoginService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProductActivationService.Services
{
    /// <summary>
    /// 管理ログイン サービス
    /// </summary>
    public class AdminLoginService(ILogger<IAdminLoginService> logger, IConfiguration configuration) : IAdminLoginService
    {
        private ILogger Logger => logger;
        private IConfiguration Configuration => configuration;

        /// <summary>
        /// 詳細取得
        /// </summary>
        /// <param name="postModel">ポストモデル</param>
        public async ValueTask<AdminLoginServiceResult> AdminLogin(AdminLoginPostModel postModel)
        {
            await Task.Delay(0);
            Logger.LogInformation("Visited:AdminLogin");
            var adminId = Configuration["AdminLogin:Id"];
            var adminPassword = Configuration["AdminLogin:Password"];
            if (adminId != postModel.Id.ToString() || adminPassword != postModel.Password)
            {
                Logger.LogInformation("IDまたはパスワードが一致しません(Id: {Id}, Password: {Password})", postModel.Id, postModel.Password);
                return new AdminLoginServiceResult(null, ServiceStatus.Unauthorized);
            }
            // JWT（アクセストークン）の作成
            var claims = new[] { new Claim(ClaimTypes.Role, "Admin") };
            var subject = new ClaimsIdentity(claims);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AdminJwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateJwtSecurityToken(
                issuer: Configuration["AdminJwt:Issuer"],
                audience: Configuration["AdminJwt:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(Configuration["AdminJwt:ExpireDays"]!)),
                subject: subject,
                signingCredentials: credentials
            );

            // 管理ログインモデルの作成
            var result = new AdminLoginModel()
            {
                AccessToken = handler.WriteToken(token),
                TokenType = "Bearer",
            };
            return new AdminLoginServiceResult(result, ServiceStatus.Ok);
        }
    }

}
