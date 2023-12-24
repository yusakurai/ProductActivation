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
        public async ValueTask<List<CustomerListModel>> GetCustomers(string? name = null)
        {
            // 設定ファイルからの取得
            Logger.LogInformation("設定ファイル「SampleKey」: {sampleKey} ", Configuration["SampleSettings:SampleKey"]);
            var customers = await Repository.GetCustomers(name);
            var result = Mapper.Map<IEnumerable<CustomerEntity>, IEnumerable<CustomerListModel>>(customers);
            return result.ToList();
        }

        /// <summary>
        /// 詳細取得
        /// </summary>
        /// <param name="id">顧客ID</param>
        public async ValueTask<CustomerDetailModel?> GetCustomer(long id)
        {
            var customer = await Repository.GetCustomerByID(id);
            if (customer == null)
            {
                return null;
            }
            return Mapper.Map<CustomerEntity, CustomerDetailModel>(customer);
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="model">顧客更新モデル</param>
        public async ValueTask<(CustomerDetailModel?, ServiceStatus)> InsertCustomer(CustomerUpdateModel model)
        {
            var entity = Mapper.Map<CustomerUpdateModel, CustomerEntity>(model);
            await Repository.InsertCustomer(entity);
            try
            {
                await Repository.Save();
            }
            catch (DbUpdateException ex)
            {
                Logger.LogWarning(ex, "customer登録時に例外発生");
                return (null, ServiceStatus.Conflict);
            }
            var resultData = await Repository.GetCustomerByID(entity.Id);
            var result = Mapper.Map<CustomerEntity, CustomerDetailModel>(resultData!);
            return (result, ServiceStatus.Ok);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id">顧客ID</param>
        /// <param name="model">顧客更新モデル</param>
        public async ValueTask<(CustomerDetailModel?, ServiceStatus)> UpdateCustomer(long id, CustomerUpdateModel model)
        {
            var entity = await Repository.GetCustomerByID(id);
            if (entity == null)
            {
                return (null, ServiceStatus.NotFound);
            }
            Mapper.Map(model, entity);
            Repository.UpdateCustomer(entity);
            try
            {
                await Repository.Save();
            }
            catch (DbUpdateException ex)
            {
                Logger.LogWarning(ex, "customer更新時に例外発生");
                return (null, ServiceStatus.Conflict);
            }
            var resultData = await Repository.GetCustomerByID(entity.Id);
            var result = Mapper.Map<CustomerEntity, CustomerDetailModel>(resultData!);
            return (result, ServiceStatus.Ok);
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="id">顧客ID</param>
        public async ValueTask<ServiceStatus> DeleteCustomer(long id)
        {
            var customer = await Repository.GetCustomerByID(id);
            if (customer == null)
            {
                return ServiceStatus.NotFound;
            }
            Repository.DeleteCustomer(customer);
            try
            {
                await Repository.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Logger.LogWarning(ex, "customer削除時に例外発生");
                return ServiceStatus.Conflict;
            }
            return ServiceStatus.Ok;
        }
    }

}
