using Taipei_YouBike_WP7.Resources;
using Taipei_YouBike_WP7.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Xml;

namespace Taipei_YouBike_WP7
{
  public partial class MainPage : PhoneApplicationPage
  {
    ProgressIndicator progressIndicator = new ProgressIndicator()
    {
      IsVisible = false,
      IsIndeterminate = true
    };

    IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;

    ObservableCollection<BikeStopViewModel> FavoriteItems;
    ObservableCollection<BikeStopViewModel> NearItems;

    bool HasFoundNear = false;
    bool IsFinding = false;

    // appbar
    ApplicationBarIconButton addIconButton;
    ApplicationBarIconButton settingsButton;
    ApplicationBarIconButton refreshIconButton;
    ApplicationBarIconButton mapsButton;

    GeoCoordinateWatcher watcher;

    private static int NearStopComparison(BikeStopViewModel a, BikeStopViewModel b)
    {
      double diff = double.Parse(b.Distance) - double.Parse(a.Distance);
      return diff > 0 ? -1 : diff == 0 ? 0 : 1;
    }

    private static double LatLngDistance(double lat1, double lng1, double lat2, double lng2)
    {
      const int R = 6371000;
      double dLat = (lat2 - lat1) * Math.PI / 180.0,
             dLng = (lng2 - lng1) * Math.PI / 180.0;
      double l1 = lat1 * Math.PI / 180.0,
             l2 = lat2 * Math.PI / 180.0;

      double a = Math.Sin(dLat / 2.0) * Math.Sin(dLat / 2.0) +
                 Math.Sin(dLng / 2.0) * Math.Sin(dLng / 2.0) * Math.Cos(l1) * Math.Cos(l2);
      double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
      return R * c;
    }

    // Constructor
    public MainPage()
    {
      InitializeComponent();

      SystemTray.SetProgressIndicator(this, progressIndicator);

      // Set the data context of the listbox control to the sample data

      FavoriteItems = new ObservableCollection<BikeStopViewModel>();
      FavoriteListView.ItemsSource = FavoriteItems;

      NearItems = new ObservableCollection<BikeStopViewModel>();
      NearList.ItemsSource = NearItems;

      BuildLocalizedApplicationBar(0);
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      if (!App.ViewModel.IsDataLoaded)
      {
        App.ViewModel.LoadData();
      }

      if (!appSettings.Contains(Constants.LOCATION_CONSENT))
      {
        MessageBoxResult result = MessageBox.Show(AppResources.LocationConsent,
            AppResources.LocationConsentCaption, MessageBoxButton.OKCancel);
        if (result == MessageBoxResult.OK)
        {
          appSettings[Constants.LOCATION_CONSENT] = true;
        }
        else
        {
          appSettings[Constants.LOCATION_CONSENT] = false;
        }

        appSettings.Save();
      }

      // fav items
      FavoriteItems.Clear();
      if (appSettings.Contains(Constants.FAVORITED_LIST))
      {
        List<int> favs = (List<int>)appSettings[Constants.FAVORITED_LIST];
        foreach (int favId in favs)
        {
          FavoriteItems.Add(App.ViewModel.Items[favId]);
        }
      }
    }

    private void BuildLocalizedApplicationBar(int page)
    {
      ApplicationBar = new ApplicationBar();

      switch (page)
      {
        case 0:
          if (addIconButton == null)
          {
            addIconButton =
                new ApplicationBarIconButton(new Uri("/Assets/AppBar/add.png", UriKind.Relative))
                {
                  Text = AppResources.Add
                };
            addIconButton.Click += (s, e) =>
            {
              NavigationService.Navigate(new Uri("/AddFavoriteStopPage.xaml", UriKind.Relative));
            };
          }

          if (settingsButton == null)
          {
            settingsButton =
                new ApplicationBarIconButton(new Uri("/Assets/AppBar/feature.settings.png", UriKind.Relative))
                {
                  Text = AppResources.Settings
                };
            settingsButton.Click += (s, e) =>
            {
              NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
            };

          }

          ApplicationBar.Buttons.Add(addIconButton);
          ApplicationBar.Buttons.Add(settingsButton);
          break;
        case 1:
          if (refreshIconButton == null)
          {
            refreshIconButton =
                new ApplicationBarIconButton(new Uri("/Assets/AppBar/sync.png", UriKind.Relative))
                {
                  Text = AppResources.Refresh,
                  IsEnabled = false
                };
            refreshIconButton.Click += (s, e) =>
            {
              FoundNear();
            };
          }

          if (mapsButton == null)
          {
            mapsButton =
                new ApplicationBarIconButton(new Uri("/Assets/Appbar/appbar.map.png", UriKind.Relative))
                {
                  Text = AppResources.AppBarMaps,
                  IsEnabled = false
                };
            mapsButton.Click += (s, e) =>
            {
              // store the near list
              appSettings["NearListTemp"] = NearItems;
              appSettings.Save();
              NavigationService.Navigate(new Uri("/MapsViewPage.xaml", UriKind.Relative));
            };
          }

          ApplicationBar.Buttons.Add(refreshIconButton);
          ApplicationBar.Buttons.Add(mapsButton);
          break;
        case 2:
          ApplicationBar.Mode = ApplicationBarMode.Minimized;
          break;
      }
    }

    private void FetchList(GeoPositionChangedEventArgs<GeoCoordinate> arg)
    {
      progressIndicator.Text = AppResources.LoadingData;
      watcher.Stop();
      GeoCoordinate location = arg.Position.Location;

      Debug.WriteLine(location.Latitude + ", " + location.Longitude);

      List<BikeStopViewModel> temp = new List<BikeStopViewModel>();
      List<string> nameList = new List<string>();

      foreach (BikeStopViewModel m in App.ViewModel.Items)
      {
        temp.Add(new BikeStopViewModel()
        {
          Name = m.Name,
          Address = m.Address,
          Availability = 0,
          Capacity = 0,
          Distance = string.Format("{0:0,0}", LatLngDistance(location.Latitude, location.Longitude, m.Latitude, m.Longitude)),
          IconType = "#FFB3E722",
          Latitude = m.Latitude,
          Longitude = m.Longitude
        });
        nameList.Add(m.Name);
      }

      // sorting
      temp.Sort(NearStopComparison);
      int len = temp.Count;
      for (int i = 0; i < len; ++i)
      {
        NearItems.Add(temp[i]);
      }

      nameList.Clear();
      temp.Clear();

      progressIndicator.IsVisible = false;
      SystemTray.SetIsVisible(this, false);

      HasFoundNear = true;
      IsFinding = false;

      refreshIconButton.IsEnabled = true;
      mapsButton.IsEnabled = true;

    }

    private void FoundNear()
    {
      if (appSettings.Contains(Constants.TRACKING) && !(bool)appSettings[Constants.TRACKING])
      {
        MessageBox.Show(AppResources.TrackingOff, AppResources.LocationConsentCaption, MessageBoxButton.OK);
        return;
      }

      if (IsFinding)
      {
        return;
      }

      IsFinding = true;

      SystemTray.SetIsVisible(this, true);
      progressIndicator.IsVisible = true;

      refreshIconButton.IsEnabled = false;
      mapsButton.IsEnabled = false;

      progressIndicator.Text = AppResources.Locating;

      watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
      watcher.MovementThreshold = 20;
      watcher.StatusChanged += (s, e) =>
      {
      };
      watcher.PositionChanged += (s, e) =>
      {
        Deployment.Current.Dispatcher.BeginInvoke(() => FetchList(e));
      };
      watcher.Start();

    }

    private void OnFavoritesSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (FavoriteListView.SelectedItem != null)
      {
        BikeStopViewModel stop = (BikeStopViewModel)FavoriteListView.SelectedItem;
        NavigationService.Navigate(new Uri("/BikeStopPage.xaml?si=" + App.ViewModel.Indecies.IndexOf(stop.Name), UriKind.Relative));

        FavoriteListView.SelectedItem = null;
      }
    }

    private void OnPanoramaItemChanged(object sender, SelectionChangedEventArgs e)
    {
      int index = MainPanorama.SelectedIndex;
      BuildLocalizedApplicationBar(index);
      switch (index)
      {
        case 1:
          if (!HasFoundNear)
          {
            FoundNear();
          }
          break;
      }
    }

    private void OnNearStopsSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (NearList.SelectedItem != null)
      {
        BikeStopViewModel stop = (BikeStopViewModel)NearList.SelectedItem;
        NavigationService.Navigate(new Uri("/BikeStopPage.xaml?si=" + App.ViewModel.Indecies.IndexOf(stop.Name), UriKind.Relative));

        NearList.SelectedItem = null;
      }
    }

    private void OnOfficialSiteTapped(object sender, System.Windows.Input.GestureEventArgs e)
    {
      WebBrowserTask officialSite = new WebBrowserTask();
      officialSite.Uri = new Uri("http://www.youbike.com.tw/");
      officialSite.Show();
    }

    private void OnSettingsTapped(object sender, System.Windows.Input.GestureEventArgs e)
    {
      NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
    }

    private void OnAboutTapped(object sender, System.Windows.Input.GestureEventArgs e)
    {
      NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
    }

    private void OnPrivacyTapped(object sender, System.Windows.Input.GestureEventArgs e)
    {
      NavigationService.Navigate(new Uri("/PrivacyPage.xaml", UriKind.Relative));

    }

  }
}