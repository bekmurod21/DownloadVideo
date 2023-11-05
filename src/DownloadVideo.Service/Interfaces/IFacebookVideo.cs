namespace DownloadVideo.Service.Interfaces;

public interface IFacebookVideo
{
    Task<string> DownloadFacebookVideoAsync(string url);
}
