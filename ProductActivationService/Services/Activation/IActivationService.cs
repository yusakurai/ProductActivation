using ProductActivationService.Models;

namespace ProductActivationService.Services
{
    /// <summary>
    /// アクティベーションサービス インターフェイス
    /// </summary>
    public interface IActivationService
    {
        enum ServiceStatus
        {
            Ok,
            Unauthorized,
        }

        ValueTask<ActivationServiceResult> Activation(ActivationPostModel postModel);
    }

    /// <summary>
    /// アクティベーションサービス 結果クラス
    /// </summary>
    public class ActivationServiceResult(ActivationModel? model, IActivationService.ServiceStatus status)
    {
        public ActivationModel? Model => model;
        public IActivationService.ServiceStatus Status => status;
    }
}
