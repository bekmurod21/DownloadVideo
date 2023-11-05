namespace DownloadVideo.Service.DTOs.Users;

public record UserForResultDto
{
    public long Id { get; set; }
    public long TelegramId { get; set; }
    public string Phone { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
}
