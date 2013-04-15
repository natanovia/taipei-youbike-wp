using Taipei_YouBike_WP7.Resources;
using Taipei_YouBike_WP7.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Maps;
using Microsoft.Phone.Shell;
using System;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Windows.Navigation;

namespace Taipei_YouBike_WP7
{
  public partial class MapsViewPage : PhoneApplicationPage
  {
    ProgressIndicator progressIndicator = new ProgressIndicator()
    {
      IsVisible = true,
      IsIndeterminate = true,
      Text = AppResources.Locating
    };
    IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;

    private GeoCoordinateWatcher watcher;

    public MapsViewPage()
    {
      InitializeComponent();

      SystemTray.SetProgressIndicator(this, progressIndicator);
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      base.OnNavigatedTo(e);

      Tracking();

      BuildLocalizedApplicationBar();
    }

    private void Tracking()
    {

      try
      {
        if (!appSettings.Contains(Constants.TRACKING) || (bool)appSettings[Constants.TRACKING])
        {
          // find your location
          SystemTray.SetIsVisible(this, true);
          progressIndicator.IsVisible = true;

          watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
          watcher.MovementThreshold = 20;
          watcher.PositionChanged += (s, e) =>
          {
            MainMapsView.Center = e.Position.Location;

            SystemTray.SetIsVisible(this, false);
            progressIndicator.IsVisible = false;

          };
          watcher.Start();

        }
        else
        {
          MainMapsView.Center = new GeoCoordinate(25.0380, 121.5487);
        }

        ObservableCollection<BikeStopViewModel> NearList = (ObservableCollection<BikeStopViewModel>)appSettings["NearListTemp"];
        foreach (BikeStopViewModel b in NearList)
        {
          Pushpin p = new Pushpin();

          p.Content = b.Name;
          p.Location = new GeoCoordinate(b.Latitude, b.Longitude);

          MarkerLayer.Children.Add(p);
        }
      }
      catch (Exception)
      {
      }
      SystemTray.SetIsVisible(this, false);
      progressIndicator.IsVisible = false;
    }

    private void BuildLocalizedApplicationBar()
    {
      ApplicationBar = new ApplicationBar();

      ApplicationBarIconButton trackMeButton =
          new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.location.circle.png", UriKind.Relative));
      trackMeButton.Text = AppResources.AppBarMyLocation;
      trackMeButton.Click += (s, e) =>
      {
        Tracking();
      };

      ApplicationBar.Buttons.Add(trackMeButton);
      ApplicationBar.Mode = ApplicationBarMode.Minimized;
    }
  }
}