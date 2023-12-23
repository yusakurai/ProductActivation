﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductActivationService.Entities;
using ProductActivationService.Models;
using ProductActivationService.Repositories;
using static ProductActivationService.Services.ICustomerService;

namespace ProductActivationService.Services
{
  /// <summary>
  /// CustomerデータCRUDサービス
  /// </summary>
  public class CustomerService(ILogger<ICustomerService> logger, ICustomerRepository repository, IMapper mapper) : ICustomerService
  {
    private ILogger Logger => logger;
    private IMapper Mapper => mapper;
    private ICustomerRepository Repository => repository;

    /// <summary>
    /// 一覧取得
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async ValueTask<List<CustomerModel>> GetCustomers(string? name = null)
    {
      var customers = await Repository.GetCustomers(name);
      var result = Mapper.Map<IEnumerable<CustomerEntity>, IEnumerable<CustomerModel>>(customers);
      return result.ToList();
    }

    /// <summary>
    /// 詳細取得
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async ValueTask<CustomerModel?> GetCustomer(long id)
    {
      var customer = await Repository.GetCustomerByID(id);
      if (customer == null)
      {
        return null;
      }
      return Mapper.Map<CustomerEntity, CustomerModel>(customer);
    }

    /// <summary>
    /// 登録
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public async ValueTask<(CustomerModel?, ServiceStatus)> InsertCustomer(InsertCustomerModel model)
    {
      var entity = Mapper.Map<InsertCustomerModel, CustomerEntity>(model);
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
      var result = Mapper.Map<CustomerEntity, CustomerModel>(resultData!);
      return (result, ServiceStatus.Ok);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public async ValueTask<(CustomerModel?, ServiceStatus)> UpdateCustomer(long id, UpdateCustomerModel model)
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
      var result = Mapper.Map<CustomerEntity, CustomerModel>(resultData!);
      return (result, ServiceStatus.Ok);
    }

    /// <summary>
    /// 削除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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