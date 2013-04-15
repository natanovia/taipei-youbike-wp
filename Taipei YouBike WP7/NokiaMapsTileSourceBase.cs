using Microsoft.Phone.Controls.Maps;
using System.Globalization;

namespace Taipei_YouBike_WP7
{
  public abstract class NokiaMapsTileSourceBase : TileSource
  {
    protected NokiaMapsTileSourceBase(string uriFormat)
      : base(uriFormat)
    {
      string loc = CultureInfo.CurrentCulture.ToString();
      if (loc.Equals("zh-TW"))
      {
        locale = "CHI";
      }
    }

    static int _tile_count = 0;

    static string locale = "ENG";
    static string token = "YOUR_HERE_MAPS_TOKEN";
    static string appId = "YOUR_HERE_MAPS_APPID";

    public override System.Uri GetUri(int x, int y, int zoomLevel)
    {
      return new System.Uri(
          string.Format(
              UriFormat,
              (_tile_count++ % 4) + 1,
              zoomLevel,
              x,
              y,
              locale,
              token,
              appId));
    }
  }

  /// <summary>
  /// 地圖檢視
  /// </summary>
  public class NokiaMapsRoadTileSource : NokiaMapsTileSourceBase
  {
    public NokiaMapsRoadTileSource()
      : base("http://{0}.maps.nlp.nokia.com/maptile/2.1/maptile/newest/normal.day/{1}/{2}/{3}/256/png8?lg={4}&token={5}&app_id={6}")
    {

    }
  }

  /// <summary>
  /// 衛星檢視 ( Satellite View )
  /// </summary>
  public class NokiaMapsSatelliteTileSource : NokiaMapsTileSourceBase
  {
    public NokiaMapsSatelliteTileSource()
      : base("http://{0}.maps.nlp.nokia.com/maptile/2.1/maptile/newest/hybrid.day/{1}/{2}/{3}/256/png8?lg={4}&token={5}&app_id={6}")
    {
    }
  }

  /// <summary>
  /// 地形檢視 ( Terrain View )
  /// </summary>
  public class NokiaMapsTerrainTileSource : NokiaMapsTileSourceBase
  {
    public NokiaMapsTerrainTileSource()
      : base("http://{0}.maps.nlp.nokia.com/maptile/2.1/maptile/newest/terrain.day/{1}/{2}/{3}/256/png8?lg={4}&token={5}&app_id={6}")
    {
    }
  }
}
