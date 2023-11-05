namespace DownloadVideo.Service.DTOs.Users;

public record UserForCreationDto
{
    public long TelegramId { get; set; }
    public string Phone { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
}
