using DownloadVideo.Service.Interfaces;

namespace DownloadVideo.Service.Services;

public class OkVideo : IOkVideo
{
    public async Task<string> DownloadOkVideoAsync(string url)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://odnoklassniki-ok-ru-video-downloader.p.rapidapi.com/"),
            Headers =
            {
                { "X-RapidAPI-Key", "2b10846b95mshddae533d6934034p17c1ccjsn9a4949169216" },
                { "X-RapidAPI-Host", "odnoklassniki-ok-ru-video-downloader.p.rapidapi.com" },
            },
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "url", $"{url}" },
            }),
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }
    }
}
