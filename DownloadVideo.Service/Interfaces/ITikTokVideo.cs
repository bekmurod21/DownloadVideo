namespace DownloadVideo.Service.Interfaces
{
    public interface ITikTokVideo
    {
        Task<string> DownloadTikTokVideoAsync(string url);
    }
}
