using ProductActivationService.Models;

namespace ProductActivationService.Services
{
    /// <summary>
    /// 管理ログインサービス インターフェイス
    /// </summary>
    public interface IAdminLoginService
    {
        enum ServiceStatus
        {
            Ok,
            Unauthorized,
        }

        ValueTask<AdminLoginServiceResult> AdminLogin(AdminLoginPostModel postModel);
    }

    /// <summary>
    /// 管理ログインサービス 結果クラス
    /// </summary>
    public class AdminLoginServiceResult(AdminLoginModel? model, IAdminLoginService.ServiceStatus status)
    {
        public AdminLoginModel? Model => model;
        public IAdminLoginService.ServiceStatus Status => status;
    }
}
