using Microsoft.EntityFrameworkCore;
using ProductActivationService.Data;
using ProductActivationService.Entities;

namespace ProductActivationService.Tests.TestData
{
    public static partial class CustomerTestData
    {
        /// <summary>
        /// カスタマーコントローラーテスト用データセットアップ
        /// </summary>
        /// <param name="context"></param>
        public static void ControllerDataSetup(MainContext context)
        {
            context.Database.OpenConnection();

            try
            {
                var entities = new CustomerEntity[]
                {
                    new () { Id = 1, Name = "TEST_DATA_01" ,ProductKey = "PK_01",LicenseLimit = 10},
                    new () { Id = 2, Name = "TEST_DATA_02" ,ProductKey = "PK_02",LicenseLimit = 10},
                    new () { Id = 3, Name = "TEST_DATA_03" ,ProductKey = "PK_03",LicenseLimit = 10},
                    new () { Id = 4, Name = "TEST_DATA_04" ,ProductKey = "PK_04",LicenseLimit = 10,DeletedAt = DateTime.Now},
                };
                context.AddRange(entities);
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Customer ON;");
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Customer OFF;");
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }
    }
}
