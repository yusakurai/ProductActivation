using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using ProductActivationService.Data;

namespace ProductActivationService.Tests.Utils
{
    /// <summary>
    /// 複数のテストクラスで前準備 (SetUp) と後始末 (TreatDown) を行うためのクラス
    /// 
    /// このクラスを継承したクラスを作成し、テストクラスに IClassFixture<継承したクラス> を指定することで、テストクラスの実行前にデータベースのセットアップを行うことができる。
    /// テストクラスのコンストラクタに前準備、Disposeに後始末処理を実装する。
    /// </summary>
    public abstract class FixtureBase
    {
        public FixtureBase()
        {
            using var context = CreateContext();
            // データベースが存在する場合に削除
            context.Database.EnsureDeleted();
            // データベースが存在しない場合に作成・スキーマ初期化（マイグレーションの代替）
            context.Database.EnsureCreated();
            // データセットアップ
            DataSetup(context);
        }

        /// <summary>
        /// Contextの作成
        /// </summary>
        /// <returns></returns>
        public MainContext CreateContext()
        {
            var optBuilder = new DbContextOptionsBuilder<MainContext>();
            optBuilder.LogTo(msg => System.Diagnostics.Debug.WriteLine(msg));
            var options = optBuilder.UseSqlServer(ConnectionString).Options;
            return new MainContext(options);
        }

        protected virtual string Host { get; } = "localhost";
        protected virtual string Database { get; } = "TSampleService";
        protected virtual string User { get; } = "sa";
        protected virtual string Password { get; } = "saPassword1234";

        /// <summary>
        /// 接続文字列
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return $"Server={Host};User ID={User};Password={Password};Initial Catalog={Database};Persist Security Info=False;TrustServerCertificate=True";
            }
        }

        /// <summary>
        /// データのセットアップ
        /// </summary>
        /// <param name="context"></param>
        protected virtual void DataSetup(MainContext context) { }

        /// <summary>
        /// HttpClientの作成
        /// </summary>
        /// <returns></returns>
        public HttpClient CreateHttpClient()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
            var application = new WebApplicationFactory<Program>();
            var client = application.CreateClient();
            return client;
        }
    }
}
