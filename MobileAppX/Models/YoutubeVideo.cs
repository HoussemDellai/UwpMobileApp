//using Google.Apis.YouTube.v3.Data;

using System;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace MobileAppX.Models
{
    public class YoutubeVideo : IItem
    {
        private SearchResult _searchResult;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string VideoId { get; set; }

        public string VideoUrl
        {
            get; set;
        }

        public TimeSpan Duration { get; set; }

        public int ViewCount { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public SearchResult SearchResult
        {
            get
            {
                return _searchResult;
            }
            set
            {
                _searchResult = value;

                UniqueId = VideoId;
            }
        }

        public string UniqueId
        {
            get; set;
        }

        public bool IsFavorit
        {
            get; set;
        }

        public bool IsWatched
        {
            get; set;
        }

        public bool IsRed { get; set; }
    }

    public interface IItem
    {

        string UniqueId
        {
            get; set;
        }

        int Id { get; set; }
    }
}
