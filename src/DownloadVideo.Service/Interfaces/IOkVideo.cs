namespace DownloadVideo.Service.Interfaces;

public interface IOkVideo
{
    Task<string> DownloadOkVideoAsync(string url);
}
