using Taipei_YouBike_WP8.Resources;

namespace Taipei_YouBike_WP8
{
  /// <summary>
  /// 提供字串資源的存取權。
  /// </summary>
  public class LocalizedStrings
  {
    private static AppResources _localizedResources = new AppResources();

    public AppResources LocalizedResources { get { return _localizedResources; } }
  }
}