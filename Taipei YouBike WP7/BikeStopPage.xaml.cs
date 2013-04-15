using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using Taipei_YouBike_WP7.Resources;
using Taipei_YouBike_WP7.ViewModels;
using System.Device.Location;
using Microsoft.Phone.Controls.Maps;
using System.Windows.Media;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Taipei_YouBike_WP7
{
  public partial class BikeStopPage : PhoneApplicationPage
  {
    ProgressIndicator progressIndicator = new ProgressIndicator()
    {
      IsVisible = true,
      IsIndeterminate = true
    };

    private string StopId;
    private int ListId;
    private IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;

    GeoCoordinateWatcher watcher = null;
    MapPolygon TargetCircle = null;
    ApplicationBarIconButton refreshButton;

    public BikeStopPage()
    {
      InitializeComponent();

      SystemTray.SetProgressIndicator(this, progressIndicator);
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      String id;

      if (NavigationContext.QueryString.TryGetValue("si", out id))
      {
        ListId = int.Parse(id);
        BikeStopViewModel stop = App.ViewModel.Items[ListId];
        GeoCoordinate location = new GeoCoordinate(stop.Latitude, stop.Longitude);
        StopId = stop.Id;
        BikeStopName.Text = stop.Name;
        BikeStopMap.Center = location;
        BikeStopDistrict.Text = stop.District;
        BikeStopAddress.Text = stop.Address;

        TargetMarker.Location = location;
        TargetMarker.Content = stop.Name;
        TargetMarker.Visibility = Visibility.Visible;

        if (!appSettings.Contains(Constants.TRACKING) || (bool)appSettings[Constants.TRACKING])
        {
          watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
          watcher.MovementThreshold = 20; // 20 meters
          watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(OnPositionChanged);
          watcher.Start();
        }

        BuildLocalizedApplicationBar();

        LoadAvailability(StopId);
      }
    }

    void OnPositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
    {
      double accuracy = e.Position.Location.HorizontalAccuracy;

      if (accuracy < e.Position.Location.VerticalAccuracy)
      {
        accuracy = e.Position.Location.VerticalAccuracy;
      }

      if (TargetCircle == null)
      {
        TargetCircle = new MapPolygon();
        TargetCircle.Opacity = 0.7;
        //Set the polygon properties 

        TargetCircle.Fill = new SolidColorBrush(Colors.Orange);
        TargetCircle.Stroke = new SolidColorBrush(Colors.Purple);
        TargetCircle.StrokeThickness = 4;

        MarkerLayer.Children.Add(TargetCircle);
      }
      Debug.WriteLine(accuracy);
      TargetCircle.Locations = CreateCircle(e.Position.Location, 50);
    }

    private void BuildLocalizedApplicationBar()
    {
      ApplicationBar = new ApplicationBar();

      bool isFavorited = false;
      List<int> favorites;
      if (appSettings.Contains(Constants.FAVORITED_LIST))
      {
        favorites = (List<int>)appSettings[Constants.FAVORITED_LIST];
        if (favorites.IndexOf(ListId) != -1)
        {
          isFavorited = true;
        }
      }

      ApplicationBarIconButton favoriteButton =
          new ApplicationBarIconButton(new Uri(isFavorited ? "/Assets/AppBar/appbar.star.minus.png" : "/Assets/AppBar/appbar.star.add.png", UriKind.Relative));
      favoriteButton.Text = isFavorited ? AppResources.BikeStopUnfavoriteButton : AppResources.BikeStopFavoriteButton;
      favoriteButton.Click += (s, e) =>
      {
        List<int> favs;

        if (isFavorited)
        {
          // unfavor
          favs = (List<int>)appSettings[Constants.FAVORITED_LIST];
          int index = favs.IndexOf(ListId);
          if (index != -1)
          {
            favs.RemoveAt(index);
          }
          favoriteButton.IconUri = new Uri("/Assets/AppBar/appbar.star.add.png", UriKind.Relative);
          favoriteButton.Text = AppResources.BikeStopFavoriteButton;
          isFavorited = false;

          MessageBox.Show(AppResources.FavoriteRemoved);
        }
        else
        {
          // fav
          if (appSettings.Contains(Constants.FAVORITED_LIST))
          {
            favs = (List<int>)appSettings[Constants.FAVORITED_LIST];
          }
          else
          {
            favs = new List<int>();
          }
          favs.Add(ListId);
          favoriteButton.IconUri = new Uri("/Assets/AppBar/appbar.star.minus.png", UriKind.Relative);
          favoriteButton.Text = AppResources.BikeStopUnfavoriteButton;
          isFavorited = true;

          MessageBox.Show(AppResources.FavoriteAdded);

        }
        appSettings[Constants.FAVORITED_LIST] = favs;
        appSettings.Save();
      };

      refreshButton =
          new ApplicationBarIconButton(new Uri("/Assets/AppBar/sync.png", UriKind.Relative))
          {
            Text = AppResources.BikeStopRefreshButton
          };
      refreshButton.Click += (s, e) =>
      {
        LoadAvailability(StopId);
      };

      ApplicationBar.Buttons.Add(favoriteButton);
      ApplicationBar.Buttons.Add(refreshButton);

      ApplicationBar.Mode = ApplicationBarMode.Minimized;
    }


    private void LoadAvailability(string stopId)
    {
      refreshButton.IsEnabled = false;
      SystemTray.SetIsVisible(this, true);
      progressIndicator.Text = AppResources.Loading;
      progressIndicator.IsVisible = true;

      try
      {
        //string result = await App.ViewModel.LoadBikeStopDataById(stopId);

        WebClient client = new WebClient();
        client.DownloadStringCompleted += (s, e) =>
        {
          if (e.Error == null)
          {
            Regex regex = new Regex(@"sbi\s*?\=\s*?'([0-9]+)?'[.\W\w]+?sus\s*?\=\s*?'([0-9]+)?';", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(e.Result);
            if (matches.Count > 0)
            {
              Match match = matches[0];
              BikeStopAvailable.Text = match.Groups[1].Value;
              BikeStopCapacity.Text = match.Groups[2].Value;
            }

          }
          else
          {
            MessageBox.Show(AppResources.NetworkError, AppResources.NetworkErrorCaption, MessageBoxButton.OK);

          }

          progressIndicator.IsVisible = false;
          SystemTray.SetIsVisible(this, false);
          refreshButton.IsEnabled = true;

        };

        client.DownloadStringAsync(new Uri("http://www.youbike.com.tw/info3b.php?sno=" + stopId, UriKind.Absolute));

      }
      catch (Exception)
      {
        MessageBox.Show(AppResources.NetworkError, AppResources.NetworkErrorCaption, MessageBoxButton.OK);

        progressIndicator.IsVisible = false;
        SystemTray.SetIsVisible(this, false);
        refreshButton.IsEnabled = true;
      }
    }

    public static double ToRadian(double degrees)
    {
      return degrees * (Math.PI / 180);
    }

    public static double ToDegrees(double radians)
    {
      return radians * (180 / Math.PI);
    }

    public static LocationCollection CreateCircle(GeoCoordinate center, double radius)
    {
      var earthRadius = 6367000; // radius in meters
      var lat = ToRadian(center.Latitude); //radians
      var lng = ToRadian(center.Longitude); //radians
      var d = radius / earthRadius; // d = angular distance covered on earth's surface
      var locations = new LocationCollection();

      for (var x = 0; x <= 360; x++)
      {
        var brng = ToRadian(x);
        var latRadians = Math.Asin(Math.Sin(lat) * Math.Cos(d) + Math.Cos(lat) * Math.Sin(d) * Math.Cos(brng));
        var lngRadians = lng + Math.Atan2(Math.Sin(brng) * Math.Sin(d) * Math.Cos(lat), Math.Cos(d) - Math.Sin(lat) * Math.Sin(latRadians));

        locations.Add(new GeoCoordinate(ToDegrees(latRadians), ToDegrees(lngRadians)));
      }

      return locations;
    }
  }
}