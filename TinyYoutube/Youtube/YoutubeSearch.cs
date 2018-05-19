using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace TinyYoutube.Youtube
{
    public delegate void VideoUpdateHandler(List<VideoInfo> videos);

    class YoutubeSearch
    {
        public event VideoUpdateHandler VideoUpdated;
        YouTubeService youtubeService;

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

            List<VideoInfo> videos = new List<VideoInfo>();

            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        {
                            videos.Add(new VideoInfo()
                            {
                                Title = searchResult.Snippet.Title,
                                Author = searchResult.Snippet.ChannelTitle,
                                Description = searchResult.Snippet.Description,
                                PublishedAt = searchResult.Snippet.PublishedAt.ToString(),
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
    }
}
