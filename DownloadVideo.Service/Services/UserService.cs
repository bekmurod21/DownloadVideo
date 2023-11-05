using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DownloadVideo.Service.DTOs.Users;
using DownloadVideo.Service.Interfaces;
using DownloadVideo.Data.IRepositories;
using DownloadVideo.Service.Exceptions;
using DownloadVideo.Service.Extensions;
using DownloadVideo.Domain.Entities.Users;

namespace DownloadVideo.Service.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> userRepository;
    private readonly IMapper mapper;

    public UserService(IRepository<User> userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<UserForResultDto> AddUserAsync(UserForCreationDto dto)
    {
        User mappedUser = this.mapper.Map<User>(dto);
        User insertedUser = await this.userRepository.InsertAsync(mappedUser);
        insertedUser.CreateAt = DateTime.UtcNow;
        return this.mapper.Map<UserForResultDto>(insertedUser);
    }

    public async Task<bool> RemoveUserAsync(long id)
    {
        User user = await this.userRepository.SelectAsync(u=>u.Id == id && !u.IsDeleted);
        if (user == null)
            throw new DownloadVideoException(404, "User not found");

        bool userResult = await this.userRepository.DeleteAsync(u => u.Id == user.Id);
        return userResult;
    }

    public async ValueTask<IEnumerable<UserForResultDto>> RetrieveAllUsersAsync(PaginationParams @params)
    {
        var users = await this.userRepository.SelectAll()
            .Where(u => !u.IsDeleted).ToPagedList(@params).AsNoTracking().ToListAsync();

        return this.mapper.Map<IEnumerable<UserForResultDto>>(users);
    }

    public async ValueTask<UserForResultDto> RetrieveUserById(long id)
    {
        var user = await this.userRepository.SelectAsync(u => u.Id == id && !u.IsDeleted);
        if (user == null)
            throw new DownloadVideoException(404, "User not Found");
        return this.mapper.Map<UserForResultDto>(user);
    }

    public async Task<UserForResultDto> ModifyUserAsync(long id,UserForCreationDto dto)
    {
        var user = await userRepository.SelectAsync(u => u.Id == id);
        if (user is null || user.IsDeleted)
            throw new DownloadVideoException(404, "Couldn't found user for given Id");

        var modifiedUser = mapper.Map(dto, user);

        return mapper.Map<UserForResultDto>(user);
    }
}
