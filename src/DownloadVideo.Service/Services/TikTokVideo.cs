using DownloadVideo.Service.Interfaces;

namespace DownloadVideo.Service.Services;

public class TikTokVideo : ITikTokVideo
{
    public async Task<string> DownloadTikTokVideoAsync(string url)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://tiktok-downloader-download-tiktok-videos-without-watermark.p.rapidapi.com/vid/index?url={url}"),
            Headers =
    {
        { "X-RapidAPI-Key", "2b10846b95mshddae533d6934034p17c1ccjsn9a4949169216" },
        { "X-RapidAPI-Host", "tiktok-downloader-download-tiktok-videos-without-watermark.p.rapidapi.com" },
    },
        };

        string body;
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();

            body = await response.Content.ReadAsStringAsync();

        }
        return body;
    }
}
