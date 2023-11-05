using DownloadVideo.Service.DTOs.Users;
using DownloadVideo.Service.Extensions;

namespace DownloadVideo.Service.Interfaces;

public interface IUserService
{
    Task<UserForResultDto> AddUserAsync(UserForCreationDto dto);
    Task<UserForResultDto> ModifyUserAsync(long id,UserForCreationDto dto);
    Task<bool> RemoveUserAsync(long id);
    ValueTask<UserForResultDto> RetrieveUserById(long id);
    ValueTask<IEnumerable<UserForResultDto>> RetrieveAllUsersAsync(PaginationParams @params);
}
