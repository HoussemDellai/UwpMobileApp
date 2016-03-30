using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Storage;
using MobileAppX.Models;
using Newtonsoft.Json;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using SQLiteNetExtensions.Extensions;

namespace MobileAppX.Repositories
{
    public class VideosRepository
    {

        //private readonly ApplicationDataContainer _roamingSettings = ApplicationData.Current.RoamingSettings;
        private readonly string _sqlpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "videosdb4.sqlite");

        public VideosRepository()
        {
            CreateDatabase();
        }

        private void CreateDatabase()
        {

            using (var connection = new SQLiteConnection(new SQLitePlatformWinRT(), _sqlpath))
            {
                connection.CreateTable<Standard>();
                connection.CreateTable<Default>();
                connection.CreateTable<High>();
                connection.CreateTable<Maxres>();
                connection.CreateTable<Medium>();

                connection.CreateTable<Thumbnails>();
                connection.CreateTable<Snippet>();
                connection.CreateTable<SearchResult>();

                connection.CreateTable<YoutubeVideo>();
            }
        }

        public List<YoutubeVideo> GetAll()
        {

            using (var connection = new SQLiteConnection(new SQLitePlatformWinRT(), _sqlpath))
            {
                var videos = connection.GetAllWithChildren<YoutubeVideo>(video => true, true).ToList();

                return videos;
            }

            //var jsonVideos = _roamingSettings.Values["videos"] as string;

            //if (jsonVideos == null)
            //{
            //    return null;
            //}

            //var videos = JsonConvert.DeserializeObject<List<YoutubeVideo>>(jsonVideos);

            //return videos;
        }

        public bool AddVideo(YoutubeVideo video)
        {

            using (var connection = new SQLiteConnection(new SQLitePlatformWinRT(), _sqlpath))
            {
                connection.InsertWithChildren(video);

                return true;
            }

            //var savedTips = GetAll();

            //if (savedTips == null)
            //{
            //    savedTips = new List<YoutubeVideo>();
            //}

            //savedTips.Add(video);

            //var jsonTips = JsonConvert.SerializeObject(savedTips);

            //_roamingSettings.Values["videos"] = jsonTips;

            //return true;
        }

        public bool RemoveVideo(YoutubeVideo video)
        {

            using (var connection = new SQLiteConnection(new SQLitePlatformWinRT(), _sqlpath))
            {
                connection.Delete(video, true);

                return true;
            }
            //var savedTips = GetAll();

            //var savedTip = savedTips.FirstOrDefault(t => t.UniqueId == tip.UniqueId);

            //if (savedTip != null)
            //{
            //    savedTips.Remove(savedTip);
            //}
            //else
            //{
            //    return false;
            //}

            //var jsonTips = JsonConvert.SerializeObject(savedTips);

            //_roamingSettings.Values["videos"] = jsonTips;

            //return true;
        }

        public bool InitiateVideos(List<YoutubeVideo> videos)
        {

            using (var connection = new SQLiteConnection(new SQLitePlatformWinRT(), _sqlpath))
            {
                //foreach (var video in videos)
                //{
                connection.InsertOrReplaceAllWithChildren(videos, true);
                //}

                return true;
            }
            //_roamingSettings.Values["videos"] = string.Empty;

            //foreach (var youtubeVideo in youtubeVideos)
            //{
            //    AddVideo(youtubeVideo);
            //}

            //return true;
        }

        public bool UpdateVideo(YoutubeVideo video)
        {
            using (var connection = new SQLiteConnection(new SQLitePlatformWinRT(), _sqlpath))
            {
                
                connection.Update(video);

                return true;
            }
        }
    }
}
