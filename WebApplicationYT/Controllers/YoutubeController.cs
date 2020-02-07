using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApplicationYT.Models;


namespace WebApplicationYT.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class YoutubeController : ApiController
    {
        // GET api/youtube
        [System.Web.Http.HttpGet]
        public List<SearchResultModified> SearchYoutubeVideo(string keyword)
        {
            //Construyendo el servicio de Youtube
            YouTubeService youtube = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyA-HQVqE6Smy-oBBk9RPrYx7jL1VwYMXTI"
            });

            SearchResource.ListRequest listRequest = youtube.Search.List("snippet");
            listRequest.Q = keyword;
            listRequest.MaxResults = 6;


            SearchListResponse searchResponse = listRequest.Execute();
            IList<SearchResult> searchResults = searchResponse.Items;
            List<SearchResultModified> searchResultCustomizeds = new List<SearchResultModified>();

            foreach (var item in searchResults.ToList())
            {
                SearchResultModified searchResultCustomized = new SearchResultModified();
                searchResultCustomized.VideoId = item.Id.VideoId;
                searchResultCustomized.Title = item.Snippet.Title;
                searchResultCustomized.ImageURL = item.Snippet.Thumbnails.Default__.Url;
                searchResultCustomizeds.Add(searchResultCustomized);
            }
            return searchResultCustomizeds;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
