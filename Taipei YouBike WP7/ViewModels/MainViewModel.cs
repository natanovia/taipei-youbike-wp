using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Taipei_YouBike_WP7.ViewModels
{
  public class MainViewModel : INotifyPropertyChanged
  {
    public MainViewModel()
    {
      this.Items = new ObservableCollection<BikeStopViewModel>();
      Indecies = new List<string>();
    }

    public ObservableCollection<BikeStopViewModel> Items { get; private set; }
    public List<string> Indecies { get; private set; }

    public bool IsDataLoaded
    {
      get;
      private set;
    }

    /// <summary>
    /// Creates and adds a few ItemViewModel objects into the Items collection.
    /// </summary>
    public void LoadData()
    {
      Items.Add(new BikeStopViewModel() { Id = "0000", Name = "捷運市政府站-1", NameEn = "MRT Taipei City Hall Stataion 1", District = "信義區", Address = "忠孝東路/松仁路(東南側)", Latitude = 25.040670, Longitude = 121.568283 });
      Items.Add(new BikeStopViewModel() { Id = "0001", Name = "捷運市政府站-2", NameEn = "MRT Taipei City Hall Stataion 2", District = "信義區", Address = "忠孝東路/松仁路(東南側)", Latitude = 25.040857, Longitude = 121.567902 });
      Items.Add(new BikeStopViewModel() { Id = "0002", Name = "捷運國父紀念館站", NameEn = "MRT S.Y.S Memorial Hall Stataion", District = "信義區", Address = "捷運國父紀念館站(3號出口)", Latitude = 25.041082, Longitude = 121.557800 });
      Items.Add(new BikeStopViewModel() { Id = "0003", Name = "台北市政府", NameEn = "Taipei City Hall", District = "信義區", Address = "台北市政府東門(松智路)", Latitude = 25.037798, Longitude = 121.565170 });
      Items.Add(new BikeStopViewModel() { Id = "0004", Name = "市民廣場", NameEn = "Citizen Square", District = "信義區", Address = "市府路/松壽路(西北側)", Latitude = 25.036036, Longitude = 121.562325 });
      Items.Add(new BikeStopViewModel() { Id = "0005", Name = "興雅國中", NameEn = "Xingya Jr. High School", District = "信義區", Address = "松仁路/松仁路95巷(東南側)", Latitude = 25.036564, Longitude = 121.568665 });
      Items.Add(new BikeStopViewModel() { Id = "0006", Name = "世貿二館", NameEn = "TWTC Exhibition Hall 2", District = "信義區", Address = "松智路/松廉路(東北側)", Latitude = 25.034737, Longitude = 121.565659 });
      Items.Add(new BikeStopViewModel() { Id = "0007", Name = "信義廣場(台北101)", NameEn = "Xinyi Square(Taipei 101)", District = "信義區", Address = "松智路/信義路(東北側)", Latitude = 25.033039, Longitude = 121.565620 });
      Items.Add(new BikeStopViewModel() { Id = "0008", Name = "世貿三館", NameEn = "TWTC Exhibition Hall 3", District = "信義區", Address = "市府路/松壽路(東南側)", Latitude = 25.035213, Longitude = 121.563690 });
      Items.Add(new BikeStopViewModel() { Id = "0009", Name = "松德站", NameEn = "Songde", District = "信義區", Address = "台北市信義區松德路300號", Latitude = 25.031590, Longitude = 121.574356 });
      Items.Add(new BikeStopViewModel() { Id = "0010", Name = "台北市災害應變中心", NameEn = "Emergency Operations Center of Taipei City", District = "信義區", Address = "台北市信義區莊敬路391巷11弄2號", Latitude = 25.028662, Longitude = 121.566116 });
      Items.Add(new BikeStopViewModel() { Id = "0011", Name = "三張犁", NameEn = "Sanchangli", District = "信義區", Address = "光復南路/基隆路一段364巷(東南側)", Latitude = 25.034937, Longitude = 121.557617 });
      Items.Add(new BikeStopViewModel() { Id = "0012", Name = "臺北醫學大學", NameEn = "Taipei Medical University", District = "信義區", Address = "台北醫學大學(吳興街220巷59弄)", Latitude = 25.026678, Longitude = 121.561745 });
      Items.Add(new BikeStopViewModel() { Id = "0013", Name = "福德公園", NameEn = "Fude Park", District = "信義區", Address = "大道路/福德街路口北西側", Latitude = 25.038090, Longitude = 121.583672 });
      Items.Add(new BikeStopViewModel() { Id = "0016", Name = "松山家商", NameEn = "Songshan Vocational High School", District = "信義區", Address = "林口街/福德街(南東側)", Latitude = 25.036083, Longitude = 121.579132 });
      Items.Add(new BikeStopViewModel() { Id = "0019", Name = "中強公園", NameEn = "Zhongqiang Park", District = "信義區", Address = "松仁路153巷17號對面", Latitude = 25.028629, Longitude = 121.569809 });
      Items.Add(new BikeStopViewModel() { Id = "0025", Name = "永吉松信路口", NameEn = "Yongji & Songxin Intersection", District = "信義區", Address = "松信路/永吉路南西側人行道", Latitude = 25.045429, Longitude = 121.572052 });
      Items.Add(new BikeStopViewModel() { Id = "0028", Name = "五常公園", NameEn = "Wuchang Park", District = "信義區", Address = "松隆路/虎林街30巷口(西南側)", Latitude = 25.048140, Longitude = 121.574669 });

      Items.Add(new BikeStopViewModel() { Id = "0014", Name = "榮星花園", NameEn = "Rongxing Park", District = "中山區", Address = "五常街/龍江路口(西南側)", Latitude = 25.064240, Longitude = 121.540367 });
      Items.Add(new BikeStopViewModel() { Id = "0034", Name = "捷運行天宮站(1號出口)", NameEn = "MRT Xingtian Temple Sta. (Exit 1)", District = "中山區", Address = "捷運行天宮1號出口後方(松江路側)", Latitude = 25.058369, Longitude = 121.532936 });
      Items.Add(new BikeStopViewModel() { Id = "0035", Name = "捷運行天宮站(3號出口)", NameEn = "MRT Xingtian Temple Sta. (Exit 3)", District = "中山區", Address = "捷運行天宮站3號出口站外", Latitude = 25.059978, Longitude = 121.533302 });
      Items.Add(new BikeStopViewModel() { Id = "0051", Name = "建國農安街口", NameEn = "Jianguo & Nongan Intersection", District = "中山區", Address = "建國北路/農安街口(中油旁邊空地)", Latitude = 25.065031, Longitude = 121.536774 });
      Items.Add(new BikeStopViewModel() { Id = "0052", Name = "建國長春路口", NameEn = "Jianguo & Changchun Intersection", District = "中山區", Address = "建國北路/長春路口(北側)", Latitude = 25.054762, Longitude = 121.536926 });
      Items.Add(new BikeStopViewModel() { Id = "0053", Name = "八德市場", NameEn = "Bade Market", District = "中山區", Address = "建國南路一段/市民大道交叉口(北側)", Latitude = 25.044781, Longitude = 121.536606 });
      Items.Add(new BikeStopViewModel() { Id = "0059", Name = "林森公園", NameEn = "Linsen Park", District = "中山區", Address = "", Latitude = 25.052227, Longitude = 121.525803 });
      Items.Add(new BikeStopViewModel() { Id = "0060", Name = "中山行政中心", NameEn = "Zhongshan Dist. Admin. Office", District = "中山區", Address = "", Latitude = 25.064318, Longitude = 121.533485 });

      Items.Add(new BikeStopViewModel() { Id = "0015", Name = "饒河夜市", NameEn = "Raohe Night Market", District = "松山區", Address = "八德路/松信路(西南側)", Latitude = 25.049845, Longitude = 121.571884 });
      Items.Add(new BikeStopViewModel() { Id = "0017", Name = "民生光復路口", NameEn = "Minsheng & Guangfu Intersection", District = "松山區", Address = "光復北路/民生東路(西北側)", Latitude = 25.058399, Longitude = 121.555038 });
      Items.Add(new BikeStopViewModel() { Id = "0018", Name = "社教館", NameEn = "Taipei Cultural Center", District = "松山區", Address = "八德路三段25號前", Latitude = 25.048267, Longitude = 121.552277 });
      Items.Add(new BikeStopViewModel() { Id = "0021", Name = "民生敦化路口", NameEn = "Minsheng & Dunhua Intersection", District = "松山區", Address = "敦化民生路口公車站旁", Latitude = 25.057985, Longitude = 121.548981 });
      Items.Add(new BikeStopViewModel() { Id = "0022", Name = "松山車站", NameEn = "Songshan Rail Sta.", District = "松山區", Address = "松山車站西出口外自行車格內", Latitude = 25.048616, Longitude = 121.578094 });
      Items.Add(new BikeStopViewModel() { Id = "0033", Name = "中崙高中", NameEn = "Zhonglun High School", District = "松山區", Address = "八德路四段91巷(中崙高中)旁", Latitude = 25.048780, Longitude = 121.560867 });
      Items.Add(new BikeStopViewModel() { Id = "0050", Name = "民權運動公園", NameEn = "MinQuan Park", District = "松山區", Address = "民權東路四段/新中街交叉口", Latitude = 25.062002, Longitude = 121.560188 });
      Items.Add(new BikeStopViewModel() { Id = "0055", Name = "臺北田徑場", NameEn = "Taipei Stadium", District = "松山區", Address = "敦化北路3號", Latitude = 25.049505, Longitude = 121.549408 });

      Items.Add(new BikeStopViewModel() { Id = "0020", Name = "捷運科技大樓站", NameEn = "MRT Technology Bldg. Sta.", District = "大安區", Address = "捷運科技大樓站對面(復興南路2段西側)", Latitude = 25.025896, Longitude = 121.543297 });
      Items.Add(new BikeStopViewModel() { Id = "0024", Name = "信義建國路口", NameEn = "Xinyi & Jianguo Intersection", District = "大安區", Address = "建國南路/信義路(西南側)", Latitude = 25.032930, Longitude = 121.537468 });
      Items.Add(new BikeStopViewModel() { Id = "0029", Name = "金山愛國路口", NameEn = "Jinshan & Aiguo Intersection", District = "大安區", Address = "愛國東路/金山南路(西南側)", Latitude = 25.031639, Longitude = 121.526550 });
      Items.Add(new BikeStopViewModel() { Id = "0030", Name = "基隆長興路口", NameEn = "Keelung & Changxing Intersection", District = "大安區", Address = "基隆路/長興街(東南側)", Latitude = 25.017054, Longitude = 121.544350 });
      Items.Add(new BikeStopViewModel() { Id = "0031", Name = "辛亥新生路口", NameEn = "Xinhai & Xinsheng Intersection", District = "大安區", Address = "辛亥路/新生南路(高架橋下)", Latitude = 25.022413, Longitude = 121.534561 });
      Items.Add(new BikeStopViewModel() { Id = "0032", Name = "捷運六張犁站", NameEn = "MRT Liuzhangli Sta.", District = "大安區", Address = "捷運出口外和平東路側", Latitude = 25.023884, Longitude = 121.553162 });
      Items.Add(new BikeStopViewModel() { Id = "0036", Name = "臺大資訊大樓", NameEn = "NTU Information Bldg.", District = "大安區", Address = "辛亥路二段(臺大外語學院外)", Latitude = 25.021009, Longitude = 121.541527 });
      Items.Add(new BikeStopViewModel() { Id = "0037", Name = "捷運東門站(4號出口)", NameEn = "MRT Dongmen Sta. (Exit 4)", District = "大安區", Address = "信義路/麗水街口", Latitude = 25.033701, Longitude = 121.529167 });
      Items.Add(new BikeStopViewModel() { Id = "0038", Name = "臺灣師範大學(圖書館)", NameEn = "NTNU Library", District = "大安區", Address = "和平東路/師大路口(北側)", Latitude = 25.026649, Longitude = 121.528893 });
      Items.Add(new BikeStopViewModel() { Id = "0045", Name = "捷運公館站(2號出口)", NameEn = "MRT Gongguan Sta.(Exit 2)", District = "大安區", Address = "羅斯福路四段/舟山路(東北側)", Latitude = 25.014759, Longitude = 121.534538 });
      Items.Add(new BikeStopViewModel() { Id = "0047", Name = "捷運忠孝新生(3號出口)", NameEn = "MRT Zhongxiao Xinsheng Sta.Exit 3 (testing)", District = "大安區", Address = "捷運忠孝新生(3號出口)", Latitude = 25.041924, Longitude = 121.533859 });
      Items.Add(new BikeStopViewModel() { Id = "0049", Name = "龍門廣場", NameEn = "Longmen Square", District = "大安區", Address = "忠孝東路/敦化南路口（西南側廣場）", Latitude = 25.040901, Longitude = 121.548248 });
      Items.Add(new BikeStopViewModel() { Id = "0054", Name = "臺北市立圖書館(總館)", NameEn = "Taipei Public Library", District = "大安區", Address = "建國南路二段/建國南路二段151巷(東北側)", Latitude = 25.028797, Longitude = 121.538071 });
      Items.Add(new BikeStopViewModel() { Id = "0057", Name = "新生和平路口", NameEn = "Xinsheng &amp; Heping Intersection", District = "大安區", Address = "新生南路二段/和平東路二段(東北側)", Latitude = 25.026217, Longitude = 121.535187 });
      Items.Add(new BikeStopViewModel() { Id = "0063", Name = "仁愛醫院", NameEn = "Taipei City Hospital Renai Branch", District = "大安區", Address = "", Latitude = 25.037569, Longitude = 121.545631 });

      Items.Add(new BikeStopViewModel() { Id = "0023", Name = "東新國小", NameEn = "Dongxin Elementary School", District = "南港區", Address = "東新國小側門(東明街62號前)", Latitude = 25.055075, Longitude = 121.602798 });
      Items.Add(new BikeStopViewModel() { Id = "0026", Name = "捷運昆陽站(1號出口)", NameEn = "MRT Kunyang Sta. (Exit 1)", District = "南港區", Address = "捷運昆陽站1號出口外停車場旁", Latitude = 25.050142, Longitude = 121.592377 });
      Items.Add(new BikeStopViewModel() { Id = "0027", Name = "捷運南港展覽館站(5號出口)", NameEn = "MRT Nangang Exhibition Center Sta. (Exit 5)", District = "南港區", Address = "研究院路/市民大道路口(西南側)", Latitude = 25.054689, Longitude = 121.616692 });
      Items.Add(new BikeStopViewModel() { Id = "0039", Name = "南港世貿公園", NameEn = "Nangang Park", District = "南港區", Address = "三重路/經貿二路88巷(東北側)", Latitude = 25.058001, Longitude = 121.614220 });
      Items.Add(new BikeStopViewModel() { Id = "0040", Name = "玉成公園", NameEn = "Yucheng Park", District = "南港區", Address = "玉成街247號前", Latitude = 25.042870, Longitude = 121.586403 });
      Items.Add(new BikeStopViewModel() { Id = "0041", Name = "中研公園", NameEn = "Academia Park", District = "南港區", Address = "研究院路二段12巷/研究院路二段12巷58弄(西南側)", Latitude = 25.047424, Longitude = 121.613708 });
      Items.Add(new BikeStopViewModel() { Id = "0042", Name = "捷運後山埤站(1號出口)", NameEn = "MRT Houshanpi Sta.(Exit 1)", District = "南港區", Address = "中坡北路/忠孝東路五段(西北側)", Latitude = 25.044310, Longitude = 121.581741 });
      Items.Add(new BikeStopViewModel() { Id = "0043", Name = "凌雲市場", NameEn = "Linyun Market", District = "南港區", Address = "研究院路三段68巷/凌雲街(東北側)", Latitude = 25.035639, Longitude = 121.614151 });
      Items.Add(new BikeStopViewModel() { Id = "0044", Name = "捷運南港軟體園區站(2號出口)", NameEn = "MRT Nangang Software Park Sta.(Exit 2)", District = "南港區", Address = "捷運南港軟體園區站2號出口外", Latitude = 25.059731, Longitude = 121.616188 });
      Items.Add(new BikeStopViewModel() { Id = "0046", Name = "南港國小", NameEn = "Nangang Elementary School", District = "南港區", Address = "惠民街/興東街(南側停車場)", Latitude = 25.056459, Longitude = 121.611031 });
      Items.Add(new BikeStopViewModel() { Id = "0048", Name = "南港車站", NameEn = "Nangang Rail Sta.", District = "南港區", Address = "忠孝東路七段與忠孝東路七段415巷交叉口", Latitude = 25.052469, Longitude = 121.608200 });

      Items.Add(new BikeStopViewModel() { Id = "0056", Name = "Y-17青少年育樂中心", NameEn = "Y17 Youth Recreation Center", District = "中正區", Address = "林森南路/仁愛路一段路口(東北側)", Latitude = 25.038954, Longitude = 121.522331 });
      Items.Add(new BikeStopViewModel() { Id = "0058", Name = "捷運善導寺站1號出口", NameEn = "MRT Shandao Temple Sta(Exit 1)", District = "中正區", Address = "天津街/忠孝東路一段(東北側)", Latitude = 25.045267, Longitude = 121.522202 });
      Items.Add(new BikeStopViewModel() { Id = "0062", Name = "南昌公園", NameEn = "Nanchang Park", District = "中正區", Address = "", Latitude = 25.026827, Longitude = 121.520256 });

      foreach (BikeStopViewModel stop in Items)
      {
        Indecies.Add(stop.Name);
      }

      this.IsDataLoaded = true;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void NotifyPropertyChanged(String propertyName)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (null != handler)
      {
        handler(this, new PropertyChangedEventArgs(propertyName));
      }
    }
  }
}