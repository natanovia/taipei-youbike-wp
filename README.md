# 我愛微笑單車 Windows Phone 7/8 App

這是[我愛微笑單車](http://www.windowsphone.com/s?appid=5daef5c9-8ec6-4af7-8828-ae63982e093c) 的開放源碼部份，與目前上架的 app 有些許的不同，但大部份是一致的。可以作為 Windows Phone 7/8 app 開發的參考。

## 編譯的準備

  * [Windows Phone 7.1/8 SDK](http://dev.windowsphone.com/downloadsdk)

    注意: 本專案是在 Windows 8 並使用 Visual Studio 2012 所編譯。(SDK 內含 Visual Studio Express 版本，可相容)

  * 我愛微笑單車使用 Windows Phone Toolkit [For WP7](http://silverlight.codeplex.com/) [For WP8](http://phone.codeplex.com/) 這套函式庫，請下載對應版本的 ``Microsoft.Phone.Controls.Toolkit.dll`` 檔案放在專案目錄下。

## 使用第三方 API

  * WP7 內使用了 Bing Maps 控制項，需要[申請 API 金鑰](http://www.microsoft.com/maps/developers/mobile.aspx)。

    在 ``BikeStopPage.xaml`` 以及 ``MapsViewPage.xaml`` 中會使用到。

  * WP7 的版本圖資以 Nokia Here Maps 提供中英文圖資，需要[申請 API 金鑰](http://www.microsoft.com/maps/developers/mobile.aspx)。

    在 ``NokiaMapsTileSourceBase.cs`` 檔案中填入。

  * WP8 的地圖控制項已經使用了最新的 Bing Maps 圖資 (也是 Nokia 圖資)，可參考[這篇文章](http://msdn.microsoft.com/en-us/library/windowsphone/develop/jj207033.aspx)了解如何取得 API 金鑰。

    在 ``BikeStopPage.xaml.cs`` 以及 ``MapsViewPage.xaml`` 中會使用到。

## 其它問題

歡迎利用 [Windows Phone 討論區](http://social.msdn.microsoft.com/Forums/zh-tw/category/windowsphoneappszhtw) 來討論相關問題。
