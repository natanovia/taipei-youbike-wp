using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace Taipei_YouBike_WP8
{
  public partial class AboutPage : PhoneApplicationPage
  {
    public AboutPage()
    {
      InitializeComponent();
    }

    private void OnRatingClicked(object sender, RoutedEventArgs e)
    {
      MarketplaceReviewTask reviewTask = new MarketplaceReviewTask();
      reviewTask.Show();
    }
  }
}