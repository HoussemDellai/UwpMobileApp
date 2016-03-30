using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using MobileAppX.Models;
using MobileAppX.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MobileAppX.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Like_OnTapped(object sender, TappedRoutedEventArgs e)
        {

            var image = (Image) sender;

            var video = image.DataContext as YoutubeVideo;

            if (video != null)
            {
                var mainViewModel = DataContext as MainViewModel;

                if (mainViewModel != null)
                {
                    mainViewModel.AddVideoToFavoriteCommand.Execute(video);
                }
            }
        }

        private void Dislike_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var image = (Image)sender;

            var video = image.DataContext as YoutubeVideo;

            if (video != null)
            {
                var mainViewModel = DataContext as MainViewModel;

                if (mainViewModel != null)
                {
                    mainViewModel.RemoveVideoFromFavoriteCommand.Execute(video);
                }
            }
        }

        private void ListViewBase_OnItemClick(object sender, ItemClickEventArgs e)
        {
            //var listView = (ListView) sender;

            //var video = listView.SelectedItem as YoutubeVideo;

            //if (video != null)
            //{
            //    var mainViewModel = DataContext as MainViewModel;

            //    if (mainViewModel != null)
            //    {
            //        mainViewModel.SelectedYoutubeVideo = video;
            //    }
            //}

            //Frame.Navigate(typeof (VideoPlayerPage));
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Frame.Navigate(typeof(VideoPlayerPage));
        }
    }
}
