using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductActivationService.Entities;
using ProductActivationService.Models;
using ProductActivationService.Repositories;
using static ProductActivationService.Services.ICustomerService;

namespace ProductActivationService.Services
{
    /// <summary>
    /// 顧客 サービス
    /// </summary>
    public class CustomerService(ILogger<ICustomerService> logger, ICustomerRepository repository, IMapper mapper, IConfiguration configuration) : ICustomerService
    {
        private ILogger Logger => logger;
        private IMapper Mapper => mapper;
        private ICustomerRepository Repository => repository;
        private IConfiguration Configuration => configuration;

        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="name">顧客名</param>
        public async ValueTask<List<CustomerListModel>> GetList(string? name = null)
        {
            // 設定ファイルからの取得
            Logger.LogInformation("設定ファイル「SampleKey」: {sampleKey} ", Configuration["SampleSettings:SampleKey"]);
            var entityList = await Repository.GetList(name);
            var modelList = Mapper.Map<IEnumerable<CustomerEntity>, IEnumerable<CustomerListModel>>(entityList);
            return modelList.ToList();
        }

        /// <summary>
        /// 詳細取得
        /// </summary>
        /// <param name="id">ID</param>
        public async ValueTask<CustomerDetailModel?> GetDetail(long id)
        {
            var entity = await Repository.GetDetail(id);
            if (entity == null)
            {
                return null;
            }
            return Mapper.Map<CustomerEntity, CustomerDetailModel>(entity);
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="model">更新モデル</param>
        public async ValueTask<(CustomerDetailModel?, ServiceStatus)> Insert(CustomerUpdateModel model)
        {
            var entity = Mapper.Map<CustomerUpdateModel, CustomerEntity>(model);
            await Repository.Insert(entity);
            try
            {
                await Repository.Save();
            }
            catch (DbUpdateException ex)
            {
                Logger.LogWarning(ex, "Customer登録時に例外発生");
                return (null, ServiceStatus.Conflict);
            }
            var newEntity = await Repository.GetDetail(entity.Id);
            var newModel = Mapper.Map<CustomerEntity, CustomerDetailModel>(newEntity!);
            return (newModel, ServiceStatus.Ok);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="model">更新モデル</param>
        public async ValueTask<(CustomerDetailModel?, ServiceStatus)> Update(long id, CustomerUpdateModel model)
        {
            var entity = await Repository.GetDetail(id);
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
                Logger.LogWarning(ex, "Customer更新時に例外発生");
                return (null, ServiceStatus.Conflict);
            }
            var newEntity = await Repository.GetDetail(entity.Id);
            var newModel = Mapper.Map<CustomerEntity, CustomerDetailModel>(newEntity!);
            return (newModel, ServiceStatus.Ok);
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="id">ID</param>
        public async ValueTask<ServiceStatus> Delete(long id)
        {
            var entity = await Repository.GetDetail(id);
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
                Logger.LogWarning(ex, "Customer削除時に例外発生");
                return ServiceStatus.Conflict;
            }
            return ServiceStatus.Ok;
        }
    }

}
