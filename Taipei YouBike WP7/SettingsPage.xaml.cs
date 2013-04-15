using System.Windows;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Controls;
using Taipei_YouBike_WP7.Resources;

namespace Taipei_YouBike_WP7
{
  public partial class SettingsPage : PhoneApplicationPage
  {
    IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;

    public SettingsPage()
    {
      InitializeComponent();

      if (appSettings.Contains(Constants.TRACKING) && !(bool)appSettings[Constants.TRACKING])
      {
        TrackLocationToggle.IsChecked = false;
      }

    }

    private void OnTrackingToggleChecked(object sender, RoutedEventArgs e)
    {
      TrackLocationToggle.Content = AppResources.On;
      appSettings[Constants.TRACKING] = true;
      appSettings.Save();
    }

    private void OnTrackingToggleUnchecked(object sender, RoutedEventArgs e)
    {
      TrackLocationToggle.Content = AppResources.Off;
      appSettings[Constants.TRACKING] = false;
      appSettings.Save();
    }
  }
}