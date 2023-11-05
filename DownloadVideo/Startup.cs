using DownloadVideo.Service.DTOs.Users;
using DownloadVideo.Service.Interfaces;

namespace DownloadVideo;
public class Startup
{
    private readonly IUserService userService;

    public Startup(IUserService userService)
    {
        this.userService = userService;
    }
    public async Task<UserForResultDto> PostAsync(UserForCreationDto user)
    {
        var addedUser = await userService.AddUserAsync(user);
        return addedUser;
    }
}
