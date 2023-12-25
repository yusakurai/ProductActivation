using System.Net.Http.Json;
using ProductActivationService.Data;
using ProductActivationService.Models;
using ProductActivationService.Tests.TestData;
using ProductActivationService.Tests.Utils;
using System.Net;

namespace ProductActivationService.Tests.Controllers
{
    public class CustomerControllerTestFixture : FixtureBase
    {
        protected override void DataSetup(MainContext context)
        {
            // 顧客テストデータのセットアップ
            CustomerTestData.ControllerDataSetup(context);
        }
    }

    public class CustomerControllerTest(CustomerControllerTestFixture fixture) : IClassFixture<CustomerControllerTestFixture>, IDisposable
    {
        // テスト対象の URL
        private static readonly string URL = "/api/v1/customer";

        // HTTP Client
        private HttpClient Client => fixture.CreateHttpClient();

        public void Dispose()
        {
            Client.Dispose();
            GC.SuppressFinalize(this);
        }

        #region GET List
        /// <summary>
        /// GET List 正常
        /// </summary>
        [Theory]
        [InlineData(0, 1, "TEST_DATA_01", "PK_01", 10)]
        [InlineData(1, 2, "TEST_DATA_02", "PK_02", 10)]
        [InlineData(2, 3, "TEST_DATA_03", "PK_03", 10)]
        public async Task GetList_200(int rowNumber, long id, string name, string productKey, int licenseLimit)
        {
            var uri = URL;
            var result = await Client.GetFromJsonAsync<List<CustomerListModel>>(uri);
            Assert.NotNull(result);
            Assert.Equal(id, result[rowNumber].Id);
            Assert.Equal(name, result[rowNumber].Name);
            Assert.Equal(productKey, result[rowNumber].ProductKey);
            Assert.Equal(licenseLimit, result[rowNumber].LicenseLimit);
        }

        /// <summary>
        /// GET List 正常 (name 指定)
        /// </summary>
        [Theory]
        [InlineData("01", 1)]
        public async Task GetListWithName_200(string name, int count)
        {
            var uri = URL + "/?name=" + name;
            var result = await Client.GetFromJsonAsync<List<CustomerListModel>>(uri);
            Assert.NotNull(result);
            Assert.Equal(count, result.Count);
        }

        /// <summary>
        /// GET List 削除済のためデータ無し (name 指定)
        /// </summary>
        [Fact]
        public async Task GetListDeleted_200_Blank()
        {
            var uri = URL + "/?name=TEST_DATA_04";
            var result = await Client.GetFromJsonAsync<List<CustomerListModel>>(uri);
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        /// <summary>
        /// GET List データ無し (name 指定)
        /// </summary>
        [Theory]
        [InlineData("TEST_DATA_NO_NAME")]
        public async Task GetListWithName_200_Blank(string name)
        {
            var uri = URL + "/?name=" + name;
            var result = await Client.GetFromJsonAsync<List<CustomerListModel>>(uri);
            Assert.NotNull(result);
            Assert.Empty(result);
        }
        #endregion

        #region GET Detail

        /// <summary>
        /// GET Detail 正常
        /// </summary>
        [Theory]
        [InlineData(1, "TEST_DATA_01", "PK_01", 10)]
        [InlineData(2, "TEST_DATA_02", "PK_02", 10)]
        [InlineData(3, "TEST_DATA_03", "PK_03", 10)]
        public async Task GetDetail_200(long id, string name, string productKey, int licenseLimit)
        {
            var uri = $"{URL}/{id}";
            var result = await Client.GetFromJsonAsync<CustomerDetailModel>(uri);
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(productKey, result.ProductKey);
            Assert.Equal(licenseLimit, result.LicenseLimit);
            Assert.NotNull(result.CreatedAt);
            Assert.NotNull(result.UpdatedAt);
            Assert.Null(result.DeletedAt);
        }

        /// <summary>
        /// GET Detail データ無し
        /// </summary>
        [Fact]
        public async Task GetDetail_404()
        {
            var uri = $"{URL}/{long.MaxValue}";
            var response = await Client.GetAsync(uri);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        #endregion

        #region POST
        /// <summary>
        /// POST 正常
        /// </summary>
        [Fact]
        public async Task Post_201()
        {
            var uri = URL;
            var model = new CustomerUpdateModel
            {
                Name = "TEST_DATA_POST",
                ProductKey = "PK_POST",
                LicenseLimit = 10
            };
            var response = await Client.PostAsJsonAsync(uri, model);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var result = await response.Content.ReadFromJsonAsync<CustomerDetailModel>();
            Assert.NotNull(result);
            Assert.Equal(model.Name, result.Name);
            Assert.Equal(model.ProductKey, result.ProductKey);
            Assert.Equal(model.LicenseLimit, result.LicenseLimit);
            Assert.NotNull(result.CreatedAt);
            Assert.NotNull(result.UpdatedAt);
            Assert.Null(result.DeletedAt);
        }

        // TODO: コンフリクトを実装
        /// <summary>
        /// POST 異常 (productKey 重複) 
        /// </summary>
        // [Fact]
        // public async Task Post_409()
        // {
        //     var uri = URL;
        //     var model = new CustomerUpdateModel
        //     {
        //         Name = "TEST_DATA_POST",
        //         ProductKey = "PK_POST",
        //         LicenseLimit = 10
        //     };
        //     var response = await Client.PostAsJsonAsync(uri, model);
        //     Assert.NotNull(response);
        //     Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        // }

        /// <summary>
        /// POST 異常 (name 未指定)
        /// </summary>
        [Fact]
        public async Task Post_400_NameBlank()
        {
            var uri = URL;
            var model = new CustomerUpdateModel
            {
                Name = "",
                ProductKey = "PK_POST",
                LicenseLimit = 10
            };
            var response = await Client.PostAsJsonAsync(uri, model);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// POST 異常 (productKey 未指定)
        /// </summary>
        [Fact]
        public async Task Post_400_ProductKeyBlank()
        {
            var uri = URL;
            var model = new CustomerUpdateModel
            {
                Name = "TEST_DATA_POST",
                ProductKey = "",
                LicenseLimit = 10
            };
            var response = await Client.PostAsJsonAsync(uri, model);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// POST 異常 (name 桁数オーバー)
        /// </summary>
        [Fact]
        public async Task Post_400_NameOverflow()
        {
            var uri = URL;
            var model = new CustomerUpdateModel
            {
                // 41桁
                Name = "12345678901234567890123456789012345678901",
                ProductKey = "PK_POST",
                LicenseLimit = 0
            };
            var response = await Client.PostAsJsonAsync(uri, model);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// POST 異常 (productKey 桁数オーバー)
        /// </summary>
        [Fact]
        public async Task Post_400_ProductKeyOverflow()
        {
            var uri = URL;
            var model = new CustomerUpdateModel
            {
                Name = "TEST_DATA_POST",
                // 17桁
                ProductKey = "12345678901234567",
                LicenseLimit = 0
            };
            var response = await Client.PostAsJsonAsync(uri, model);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        #endregion

        #region PUT
        /// <summary>
        /// PUT 正常
        /// </summary>
        [Fact]
        public async Task Put_200()
        {
            // 更新用にデータを登録
            var uriPost = $"{URL}";
            var modelPost = new CustomerUpdateModel
            {
                Name = "TEST_DATA_POST",
                ProductKey = "PK_POST",
                LicenseLimit = 10
            };
            var responsePost = await Client.PostAsJsonAsync(uriPost, modelPost);
            var resultPost = await responsePost.Content.ReadFromJsonAsync<CustomerDetailModel>();
            Assert.NotNull(resultPost);

            // 更新処理
            var uriPut = $"{URL}/{resultPost.Id}";
            var model = new CustomerUpdateModel
            {
                Name = "TEST_DATA_PUT",
                ProductKey = "PK_PUT",
                LicenseLimit = 20
            };
            var response = await Client.PutAsJsonAsync(uriPut, model);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadFromJsonAsync<CustomerDetailModel>();
            Assert.NotNull(result);
            Assert.Equal(model.Name, result.Name);
            Assert.Equal(model.ProductKey, result.ProductKey);
            Assert.Equal(model.LicenseLimit, result.LicenseLimit);
            Assert.NotNull(result.CreatedAt);
            Assert.NotNull(result.UpdatedAt);
            Assert.Null(result.DeletedAt);
        }

        /// <summary>
        /// PUT 異常 (name 未指定)
        /// </summary>
        [Fact]
        public async Task Put_400_NameBlank()
        {
            var uri = $"{URL}/{1}";
            var model = new CustomerUpdateModel
            {
                Name = "",
                ProductKey = "PK_PUT",
                LicenseLimit = 10
            };
            var response = await Client.PutAsJsonAsync(uri, model);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// PUT 異常 (productKey 未指定)
        /// </summary>
        [Fact]
        public async Task Put_400_ProductKeyBlank()
        {
            var uri = $"{URL}/{1}";
            var model = new CustomerUpdateModel
            {
                Name = "TEST_DATA_PUT",
                ProductKey = "",
                LicenseLimit = 10
            };
            var response = await Client.PutAsJsonAsync(uri, model);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// PUT 異常 (name 桁数オーバー)
        /// </summary>
        [Fact]
        public async Task Put_400_NameOverflow()
        {
            var uri = $"{URL}/{1}";
            var model = new CustomerUpdateModel
            {
                // 41桁
                Name = "12345678901234567890123456789012345678901",
                ProductKey = "PK_PUT",
                LicenseLimit = 0
            };
            var response = await Client.PutAsJsonAsync(uri, model);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// PUT 異常 (productKey 桁数オーバー)
        /// </summary>
        [Fact]
        public async Task Put_400_ProductKeyOverflow()
        {
            var uri = $"{URL}/{1}";
            var model = new CustomerUpdateModel
            {
                Name = "TEST_DATA_PUT",
                // 17桁
                ProductKey = "12345678901234567",
                LicenseLimit = 0
            };
            var response = await Client.PutAsJsonAsync(uri, model);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// PUT 異常 (データ無し)
        /// </summary>
        [Fact]
        public async Task Put_404()
        {
            var uri = $"{URL}/{long.MaxValue}";
            var model = new CustomerUpdateModel
            {
                Name = "TEST_DATA_PUT",
                ProductKey = "PK_PUT",
                LicenseLimit = 10
            };
            var response = await Client.PutAsJsonAsync(uri, model);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        // TODO: コンフリクトを実装
        /// <summary>
        /// PUT 異常 (productKey 重複)
        /// </summary>
        // [Fact]
        // public async Task Put_409()
        // {
        //     var uri = $"{URL}/{1}";
        //     var model = new CustomerUpdateModel
        //     {
        //         Name = "TEST_DATA_PUT",
        //         ProductKey = "PK02",
        //         LicenseLimit = 10
        //     };
        //     var response = await Client.PutAsJsonAsync(uri, model);
        //     Assert.NotNull(response);
        //     Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        // }
        #endregion

        #region DELETE
        /// <summary>
        /// DELETE 正常
        /// </summary>
        [Fact]
        public async Task Delete_200()
        {
            // 削除用にデータを登録
            var uriPost = $"{URL}";
            var modelPost = new CustomerUpdateModel
            {
                Name = "TEST_DATA_DELETE",
                ProductKey = "PK_DELETE",
                LicenseLimit = 10
            };
            var responsePost = await Client.PostAsJsonAsync(uriPost, modelPost);
            var resultPost = await responsePost.Content.ReadFromJsonAsync<CustomerDetailModel>();
            Assert.NotNull(resultPost);

            // 削除処理
            var uriDelete = $"{URL}/{resultPost.Id}";
            var responseDelete = await Client.DeleteAsync(uriDelete);
            Assert.NotNull(responseDelete);
            Assert.Equal(HttpStatusCode.NoContent, responseDelete.StatusCode);

            // 削除データの取得
            var uriGet = $"{URL}/{resultPost.Id}";
            var responseGet = await Client.GetAsync(uriGet);
            Assert.NotNull(responseGet);
            var resultGet = await responseGet.Content.ReadFromJsonAsync<CustomerDetailModel>();
            Assert.NotNull(resultGet);
            Assert.NotNull(resultGet.DeletedAt);
        }

        /// <summary>
        /// DELETE 異常 (データ無し)
        /// </summary>
        [Fact]
        public async Task Delete_404()
        {
            var uri = $"{URL}/{long.MaxValue}";
            var response = await Client.DeleteAsync(uri);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        #endregion
    }
}
