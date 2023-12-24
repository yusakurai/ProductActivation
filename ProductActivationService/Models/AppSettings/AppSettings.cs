namespace ProductActivationService.Models
{
  /// <summary>
  /// Application.jsonモデル
  /// </summary>
  public class AppSettings
  {
    /// <summary>
    /// Sample設定
    /// </summary>
    /// <value></value>
    public SampleSettingsModel SampleSettings { get; set; } = new SampleSettingsModel();
    /// <summary>
    /// SampleSettingsモデル
    /// </summary>
    public class SampleSettingsModel
    {
      /// <summary>
      /// サンプルキー
      /// </summary>
      public string SampleKey { get; set; } = "This is SampleKey";
    }
  }

}
