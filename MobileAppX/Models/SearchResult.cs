using System;
using Newtonsoft.Json;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace MobileAppX.Models
{

    public class SearchResult
    {

        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Etag { get; set; }
        public string Kind { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Snippet Snippet { get; set; }

        [ForeignKey(typeof(YoutubeVideo))]
        public int YoutubeVideoId { get; set; }
    }

    public class Snippet
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string ChannelId { get; set; }
        public string ChannelTitle { get; set; }
        public string Description { get; set; }
        public string LiveBroadcastContent { get; set; }
        public DateTime PublishedAt { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Thumbnails Thumbnails { get; set; }
        public string Title { get; set; }

        [ForeignKey(typeof(SearchResult))]
        public int SearchResultId { get; set; }
    }

    public class Thumbnails
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Default Default { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public High High { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Maxres Maxres { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Medium Medium { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Standard Standard { get; set; }

        [ForeignKey(typeof(Snippet))]
        public int SnippetId { get; set; }
    }

    public class Default
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int? Height { get; set; }
        public string Url { get; set; }
        public int? Width { get; set; }

        [ForeignKey(typeof(Thumbnails))]
        public int ThumbnailsId { get; set; }
    }

    public class High
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int? Height { get; set; }
        public string Url { get; set; }
        public int? Width { get; set; }

        [ForeignKey(typeof(Thumbnails))]
        public int ThumbnailsId { get; set; }
    }

    public class Maxres
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int? Height { get; set; }
        public string Url { get; set; }
        public int? Width { get; set; }
        public string ETag { get; set; }

        [ForeignKey(typeof(Thumbnails))]
        public int ThumbnailsId { get; set; }
    }

    public class Medium
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int? Height { get; set; }
        public string Url { get; set; }
        public int? Width { get; set; }

        [ForeignKey(typeof(Thumbnails))]
        public int ThumbnailsId { get; set; }
    }

    public class Standard
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int? Height { get; set; }
        public string Url { get; set; }
        public int? Width { get; set; }

        [ForeignKey(typeof(Thumbnails))]
        public int ThumbnailsId { get; set; }
    }

    //public class SearchResult
    //{
    //    public string Etag { get; set; }
    //    public Id Id { get; set; }
    //    public string Kind { get; set; }
    //    public Snippet Snippet { get; set; }
    //}

    //public class Default
    //{
    //    public int Height { get; set; }
    //    public string Url { get; set; }
    //    public int Width { get; set; }
    //    public object ETag { get; set; }
    //}

    //public class High
    //{
    //    public int Height { get; set; }
    //    public string Url { get; set; }
    //    public int Width { get; set; }
    //    public object ETag { get; set; }
    //}

    //public class Id
    //{
    //    public object ChannelId { get; set; }
    //    public string Kind { get; set; }
    //    public object PlaylistId { get; set; }
    //    public string VideoId { get; set; }
    //    public object ETag { get; set; }
    //}

    //public class Medium
    //{
    //    public int Height { get; set; }
    //    public string Url { get; set; }
    //    public int Width { get; set; }
    //    public object ETag { get; set; }
    //}

    //public class Snippet
    //{
    //    public string ChannelId { get; set; }
    //    public string ChannelTitle { get; set; }
    //    public string Description { get; set; }
    //    public string LiveBroadcastContent { get; set; }
    //    public DateTime PublishedAt { get; set; }
    //    public Thumbnails Thumbnails { get; set; }
    //    public string Title { get; set; }
    //    public object ETag { get; set; }
    //}

    //public class Thumbnails
    //{
    //    public Default Default__ { get; set; }
    //    public High High { get; set; }
    //    public object Maxres { get; set; }
    //    public Medium Medium { get; set; }
    //    public object Standard { get; set; }
    //    public object ETag { get; set; }
    //}
}