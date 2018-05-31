using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Net;

using Google.Apis.Services;
using Google.Apis.YouTube.v3;

using YoutubeListView;

namespace TinyYoutube.Youtube
{
    public delegate void VideoUpdateHandler(List<VideoInfo> videos);
    public delegate void ImageUpdateHandler(Image srcImage, YoutubeItem item);

    class YoutubeSearch
    {
        #region Variables

        public event VideoUpdateHandler VideoUpdated;
        public event ImageUpdateHandler ImageUpdated;

        YouTubeService youtubeService;
        WebClient wc = new WebClient();

        #endregion

        #region Main Functions

        public YoutubeSearch()
        {
            youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyBJxv0OjdlgR3S7YvfQa4xgeujICQSXdhQ",
                ApplicationName = "TinyYoutube"
            });
        }

        public async Task search(string query, int maxResult)
        {
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = query;
            searchListRequest.MaxResults = maxResult;

            // call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            if (searchListResponse == null) return;

            List<VideoInfo> videos = new List<VideoInfo>();

            foreach (var searchResult in searchListResponse.Items)
            {
                if (searchResult == null || searchResult.Id == null) continue;

                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        {
                            videos.Add(new VideoInfo()
                            {
                                Id = searchResult.Id.VideoId,
                                Title = searchResult.Snippet.Title,
                                Author = searchResult.Snippet.ChannelTitle,
                                Description = searchResult.Snippet.Description,
                                PublishedAt = searchResult.Snippet.PublishedAt.Value.Month + "/" + searchResult.Snippet.PublishedAt.Value.Year,
                                Duration = "00:00",
                                Url = "https://www.youtube.com/watch?v=" + searchResult.Id.VideoId,
                                Thumbnail = searchResult.Snippet.Thumbnails.Default__.Url,
                            });
                            break;
                        }
                }
            }

            VideoUpdated(videos);
        }

        public async Task readImageFromUrl(string url, YoutubeItem item)
        {
            byte[] bytes = wc.DownloadData(url);
            MemoryStream ms = new MemoryStream(bytes);
            Image srcImage = Image.FromStream(ms);
            ImageUpdated(srcImage, item);
        }

        public Image readImageFromUrl(string url)
        {
            byte[] bytes = wc.DownloadData(url);
            MemoryStream ms = new MemoryStream(bytes);
            return Image.FromStream(ms);
        }

        #endregion
    }
}
