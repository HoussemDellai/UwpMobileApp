using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.PlayerFramework;
using MobileAppX.Models;
using MobileAppX.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MobileAppX.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VideoPlayerPage : Page
    {
        public VideoPlayerPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var mainViewModel = DataContext as MainViewModel;

            if (mainViewModel != null)
            {
                mainViewModel.AddVideoToWatchedCommand.Execute(mainViewModel.SelectedYoutubeVideo);
            }
        }

        private void Like_OnTapped(object sender, TappedRoutedEventArgs e)
        {

            var image = (Image)sender;

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

        private void MediaPlayer_OnIsFullScreenChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            var mp = (MediaPlayer)sender;

            mp.IsFullWindow = true;
        }
    }
}
