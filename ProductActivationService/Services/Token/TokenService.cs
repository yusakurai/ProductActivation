using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductActivationService.Entities;
using ProductActivationService.Models;
using ProductActivationService.Repositories;
using static ProductActivationService.Services.ITokenService;

namespace ProductActivationService.Services
{
    /// <summary>
    /// トークン サービス
    /// </summary>
    public class TokenService(ILogger<ITokenService> logger, ITokenRepository repository, IMapper mapper) : ITokenService
    {
        private ILogger Logger => logger;
        private IMapper Mapper => mapper;
        private ITokenRepository Repository => repository;

        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="sub">sub</param>
        public async ValueTask<List<TokenListModel>> GetList(string? sub = null)
        {
            var entityList = await Repository.GetList(sub);
            var modelList = Mapper.Map<IEnumerable<TokenEntity>, IEnumerable<TokenListModel>>(entityList);
            return modelList.ToList();
        }

        /// <summary>
        /// 詳細取得
        /// </summary>
        /// <param name="sub">sub</param>
        public async ValueTask<TokenDetailModel?> GetDetail(string sub)
        {
            var entity = await Repository.GetDetail(sub);
            if (entity == null)
            {
                return null;
            }
            return Mapper.Map<TokenEntity, TokenDetailModel>(entity);
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="model">更新モデル</param>
        public async ValueTask<(TokenDetailModel?, ServiceStatus)> Insert(TokenUpdateModel model)
        {
            var entity = Mapper.Map<TokenUpdateModel, TokenEntity>(model);
            await Repository.Insert(entity);
            try
            {
                await Repository.Save();
            }
            catch (DbUpdateException ex)
            {
                Logger.LogWarning(ex, "Token登録時に例外発生");
                return (null, ServiceStatus.Conflict);
            }
            var newEntity = await Repository.GetDetail(entity.Sub.ToString());
            var newModel = Mapper.Map<TokenEntity, TokenDetailModel>(newEntity!);
            return (newModel, ServiceStatus.Ok);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sub">sub</param>
        /// <param name="model">更新モデル</param>
        public async ValueTask<(TokenDetailModel?, ServiceStatus)> Update(string sub, TokenUpdateModel model)
        {
            var entity = await Repository.GetDetail(sub);
            if (entity == null)
            {
                return (null, ServiceStatus.NotFound);
            }
            Mapper.Map(model, entity);
            Repository.Update(entity);
            try
            {
                await Repository.Save();
            }
            catch (DbUpdateException ex)
            {
                Logger.LogWarning(ex, "Token更新時に例外発生");
                return (null, ServiceStatus.Conflict);
            }
            var newEntity = await Repository.GetDetail(entity.Sub.ToString());
            var newModel = Mapper.Map<TokenEntity, TokenDetailModel>(newEntity!);
            return (newModel, ServiceStatus.Ok);
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sub">sub</param>
        public async ValueTask<ServiceStatus> Delete(string sub)
        {
            var entity = await Repository.GetDetail(sub);
            if (entity == null)
            {
                return ServiceStatus.NotFound;
            }
            Repository.Delete(entity);
            try
            {
                await Repository.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Logger.LogWarning(ex, "Token削除時に例外発生");
                return ServiceStatus.Conflict;
            }
            return ServiceStatus.Ok;
        }
    }

}
