using DownloadVideo.Domain.Commons;

namespace DownloadVideo.Domain.Entities.Users;

public class User:Auditable
{
    public long TelegramId { get; set; }
    public string Phone { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
}