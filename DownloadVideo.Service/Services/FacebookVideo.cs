using DownloadVideo.Service.Interfaces;

namespace DownloadVideo.Service.Services;

public class FacebookVideo : IFacebookVideo
{
    public async Task<string> DownloadFacebookVideoAsync(string url)
    {
        using (var client = new HttpClient())
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://fb-video-reels.p.rapidapi.com/api/getSocialVideo?url={url}"),
                Headers =
                {
                    { "X-RapidAPI-Key", "2b10846b95mshddae533d6934034p17c1ccjsn9a4949169216" },
                    { "X-RapidAPI-Host", "fb-video-reels.p.rapidapi.com" },
                },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return body;
            }
        }
    }
}
