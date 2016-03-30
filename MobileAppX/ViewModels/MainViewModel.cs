using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;
using GalaSoft.MvvmLight.Command;
using MobileAppX.Models;
using MobileAppX.Repositories;
using MobileAppX.Services;

namespace MobileAppX.ViewModels
{
    /// <summary>
    /// The main ViewModel that will be bound to MainView page.
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private List<YoutubeVideo> _youtubeVideos;
        private YoutubeVideo _selectedYoutubeVideo;
        private bool _isBusy;
        private int _numberOfWatchedVideos;
        private int _numberOfFavoriteVideos;
        private int _numberOfTotalVideos;
        private int _percentageOfWatchedVideos;
        private int _percentageOfFavoriteVideos;

        private readonly VideosRepository _videosRepository = new VideosRepository();
        private List<Tweet> _tweetsList;
        private List<YoutubeVideo> _favouriteVideos;
        private List<YoutubeVideo> _watchedVideos;

        public List<YoutubeVideo> YoutubeVideos
        {
            get { return _youtubeVideos; }
            set
            {
                _youtubeVideos = value;
                OnPropertyChanged();
            }
        }

        public List<YoutubeVideo> FavouriteVideos
        {
            get { return _favouriteVideos; }
            set
            {
                _favouriteVideos = value;
                OnPropertyChanged();
            }
        }

        public List<YoutubeVideo> WatchedVideos
        {
            get { return _watchedVideos; }
            set
            {
                _watchedVideos = value;
                OnPropertyChanged();
            }
        }

        public YoutubeVideo SelectedYoutubeVideo
        {
            get
            {
                return _selectedYoutubeVideo;
            }
            set
            {
                _selectedYoutubeVideo = value;
                OnPropertyChanged();
            }
        }

        public List<Tweet> TweetsList
        {
            get { return _tweetsList; }
            set
            {
                _tweetsList = value;
                OnPropertyChanged();
            }
        }


        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public int NumberOfWatchedVideos
        {
            get { return _numberOfWatchedVideos; }
            set
            {
                _numberOfWatchedVideos = value;
                OnPropertyChanged();
            }
        }

        public int NumberOfFavoriteVideos
        {
            get { return _numberOfFavoriteVideos; }
            set
            {
                _numberOfFavoriteVideos = value;
                OnPropertyChanged();
            }
        }

        public int NumberOfTotalVideos
        {
            get { return _numberOfTotalVideos; }
            set
            {
                _numberOfTotalVideos = value;
                OnPropertyChanged();
            }
        }

        public int PercentageOfWatchedVideos
        {
            get { return _percentageOfWatchedVideos; }
            set
            {
                _percentageOfWatchedVideos = value;
                OnPropertyChanged();
            }
        }

        public int PercentageOfFavoriteVideos
        {
            get { return _percentageOfFavoriteVideos; }
            set
            {
                _percentageOfFavoriteVideos = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddVideoToWatchedCommand
        {
            get
            {
                return new RelayCommand<YoutubeVideo>(video =>
                {
                    video.IsWatched = true;
                    _videosRepository.UpdateVideo(video);
                });
            }
        }

        public ICommand AddVideoToFavoriteCommand
        {
            get
            {
                return new RelayCommand<YoutubeVideo>(video =>
                {
                    video.IsFavorit = true;
                    _videosRepository.UpdateVideo(video);
                });
            }
        }

        public ICommand RemoveVideoFromFavoriteCommand
        {
            get
            {
                return new RelayCommand<YoutubeVideo>(video =>
                {
                    video.IsFavorit = false;
                    _videosRepository.UpdateVideo(video);
                });
            }
        }

        //public ICommand ShareVideoCommand
        //{
        //    get
        //    {
        //        return new Command<YoutubeVideo>(video =>
        //        {
        //            if (video != null)
        //            {
        //                CrossShare.Current.ShareLink("https://www.youtube.com/watch?v=" +
        //                    video.SearchResult.Snippet.Thumbnails.Default__.Url.Replace("https://i.ytimg.com/vi/", "")
        //                        .Replace("/default.jpg", ""),
        //                    "Lots of fun !",
        //                    video.SearchResult.Snippet.Title);
        //            }
        //        });
        //    }
        //}

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    await LoadDataAsync();
                });
            }
        }

        //public ICommand SyncCommand
        //{
        //    get
        //    {
        //        return new Command(async () =>
        //        {
        //            await DownloadDataAsync();
        //        });
        //    }
        //}

        public MainViewModel()
        {
            //if (DesignMode.DesignModeEnabled)
            //{
            //    DownloadDataAsync();
            //}
            //else
            //{
            LoadDataAsync();
            //}
        }

        private async Task LoadDataAsync()
        {
            IsBusy = true;

            TweetsList = await new DataServices().GetTweetsAsync();

            var videos = _videosRepository.GetAll();

            if (videos?.Count == 0)
            {
                await DownloadDataAsync();
            }
            else
            {
                YoutubeVideos = videos;

                FavouriteVideos = videos.Where(video => video.IsFavorit).ToList();

                WatchedVideos = videos.Where(video => video.IsWatched).ToList();

                SetStatistics();
            }

            IsBusy = false;
        }

        public async Task DownloadDataAsync()
        {

            IsBusy = true;

            var employeesServices = new DataServices();

            var videos = await employeesServices.GetVideosAsync();

            videos = videos.OrderByDescending(video => video.SearchResult.Snippet.PublishedAt).ToList();

            YoutubeVideos = SetVideosUniqueId(videos);

            _videosRepository.InitiateVideos(_youtubeVideos);

            SetStatistics();

            IsBusy = false;
        }

        private void SetStatistics()
        {
            if (_youtubeVideos.Any())
            {
                NumberOfWatchedVideos = _youtubeVideos.Count(video => video.IsWatched);

                NumberOfFavoriteVideos = _youtubeVideos.Count(video => video.IsFavorit);

                NumberOfTotalVideos = _youtubeVideos.Count;

                PercentageOfWatchedVideos = _numberOfWatchedVideos * 100 / _numberOfTotalVideos;

                PercentageOfFavoriteVideos = _numberOfFavoriteVideos * 100 / _numberOfTotalVideos;
            }
        }

        private List<YoutubeVideo> SetVideosUniqueId(List<YoutubeVideo> youtubeVideos)
        {

            for (int i = 0; i < youtubeVideos.Count; i++)
            {
                youtubeVideos[i].Id = i;
            }

            return youtubeVideos;
        }

        //public bool AddVideoToFavorit(YoutubeVideo youtubeVideo)
        //{

        //    if (youtubeVideo.IsFavorit)
        //    {
        //        return false;
        //    }

        //    var video = _youtubeVideos.FirstOrDefault(v => v.UniqueId == youtubeVideo.UniqueId);

        //    video.IsFavorit = true;

        //    return _videosRepository.InitiateVideos(_youtubeVideos);
        //}

        //public bool RemoveVideoFromFavorit(YoutubeVideo youtubeVideo)
        //{

        //    if (!youtubeVideo.IsFavorit)
        //    {
        //        return false;
        //    }

        //    var video = _youtubeVideos.FirstOrDefault(v => v.UniqueId == youtubeVideo.UniqueId);

        //    video.IsFavorit = false;

        //    return _videosRepository.InitiateVideos(_youtubeVideos);
        //    //var tipsRepository = new FavoritVideosRepository();

        //    //tip.IsFavorit = false;

        //    //var isTipRemoved = tipsRepository.RemoveTip(tip);

        //    //if (isTipRemoved)
        //    //{
        //    //    return true;
        //    //}

        //    //return false;
        //}

        //public bool AddVideoToIsWatched(YoutubeVideo youtubeVideo)
        //{

        //    if (youtubeVideo.IsWatched)
        //    {
        //        return false;
        //    }

        //    var video = _youtubeVideos.FirstOrDefault(v => v.UniqueId == youtubeVideo.UniqueId);

        //    video.IsWatched = true;

        //    return _videosRepository.InitiateVideos(_youtubeVideos);

        //    //if (youtubeVideo.IsWatched)
        //    //{
        //    //    return false;
        //    //}

        //    //var genericRepository = new GenericRepository<YoutubeVideo>(Settings.WatchedVideosSettingsKey);

        //    //var isAdded = genericRepository.Add(youtubeVideo);

        //    //if (isAdded)
        //    //{

        //    //    youtubeVideo.IsWatched = true;

        //    //    return true;
        //    //}

        //    //return false;
        //}

        //public bool RemoveVideoFromIsWatched(YoutubeVideo youtubeVideo)
        //{

        //    if (!youtubeVideo.IsWatched)
        //    {
        //        return false;
        //    }

        //    var video = _youtubeVideos.FirstOrDefault(v => v.UniqueId == youtubeVideo.UniqueId);

        //    video.IsWatched = false;

        //    return _videosRepository.InitiateVideos(_youtubeVideos);

        //    //var genericRepository = new GenericRepository<YoutubeVideo>(Settings.WatchedVideosSettingsKey);

        //    //var isRemoved = genericRepository.Remove(youtubeVideo);

        //    //if (isRemoved)
        //    //{

        //    //    youtubeVideo.IsWatched = false;

        //    //    return true;
        //    //}

        //    //return false;
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    ///// A command whose sole purpose is to relay its functionality to other objects by invoking delegates.
    ///// </summary>
    //public class RelayCommand : ICommand
    //{
    //    #region Fields

    //    readonly Action<object> _execute;
    //    readonly Predicate<object> _canExecute;

    //    #endregion

    //    #region Constructors

    //    /// <summary>
    //    /// Creates a new command that can always execute.
    //    /// </summary>
    //    /// <param name="execute">The execution logic.</param>
    //    public RelayCommand(Action<object> execute)
    //    {
    //        this(execute, null);
    //    }

    //    /// <summary>
    //    /// Creates a new command.
    //    /// </summary>
    //    /// <param name="execute">The execution logic.</param>
    //    /// <param name="canExecute">The execution status logic.</param>
    //    public RelayCommand(Action<object> execute, Predicate<object> canExecute)
    //    {
    //        if (execute == null)
    //            throw new ArgumentNullException("execute");

    //        _execute = execute;
    //        _canExecute = canExecute;
    //    }
    //    #endregion

    //    #region ICommand Members

    //    /// <summary>
    //    /// Sets Can Execute Property to True or False
    //    /// </summary>
    //    /// <param name="parameter"></param>
    //    /// <returns>Whether or not the action can execute</returns>
    //    /// <remarks>
    //    /// Uses DebuggerStepThrough from System.Diagnostics
    //    /// CanExecute can happen a lot as the UI checks if it CanExecute something.
    //    /// Debugger will step over unless there is an explicit break point inside of it
    //    /// </remarks>
    //    [DebuggerStepThrough()]
    //    public bool CanExecute(object parameter)
    //    {
    //        return _canExecute == null ? true : _canExecute(parameter);
    //    }

    //    /// <summary>
    //    /// Listener when CanExecute Property Changes
    //    /// </summary>
    //    public event EventHandler CanExecuteChanged;

    //    /// <summary>
    //    /// Launches Execute ICommand
    //    /// </summary>
    //    /// <param name="parameter"></param>
    //    public void Execute(object parameter)
    //    {
    //        _execute(parameter);
    //    }

    //    #endregion
    //}
}
