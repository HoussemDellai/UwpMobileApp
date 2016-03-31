using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MobileAppX.Models;
using Newtonsoft.Json;

//using Xamarin.Forms;

namespace MobileAppX.Services
{
    public class DataServices
    {

        /// <summary>
        /// Gets a list of all the employees.
        /// </summary>
        /// <returns></returns>
        public async Task<List<YoutubeVideo>> GetVideosAsync()
        {
            var videosUrl = "http://socialmediaservices.azurewebsites.net/api/youtube/playlist/v2/UUKUOmGXE9Ytlc2EzpGqimtw/50";

            //"http://socialmediaservices.azurewebsites.net/api/youtube/playlist/v2/UUKUOmGXE9Ytlc2EzpGqimtw/5";

            //"http://socialmediaservices.azurewebsites.net/api/youtube/playlist/UC7-i0Cfhv42n1BVfrFuDcPA/50";

            var httpClient = new HttpClient();

            try
            {
                var json = await httpClient.GetStringAsync(videosUrl);

                var result = JsonConvert.DeserializeObject<List<YoutubeVideo>>(json);

                return result;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<List<Tweet>> GetTweetsAsync()
        {
            var client = new HttpClient();

            try
            {

                var tweetsJson = await client.GetStringAsync("http://houssemdellai-services.azurewebsites.net/api/tweets/PeterSage007/PeterSage007/20");

                return JsonConvert.DeserializeObject<List<Tweet>>(tweetsJson);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Data);
                Debug.WriteLine(exc.Message);
                Debug.WriteLine(exc.InnerException);

                return null;
            }
        }
    }
}
