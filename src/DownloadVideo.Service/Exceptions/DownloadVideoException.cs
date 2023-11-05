namespace DownloadVideo.Service.Exceptions;

public class DownloadVideoException:Exception
{
    public short Code { get;set; }
    public DownloadVideoException(short code,string message):base(message)
    {
        Code = code;
    }
}
