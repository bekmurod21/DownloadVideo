using AutoMapper;
using DownloadVideo.Domain.Entities.Users;
using DownloadVideo.Service.DTOs.Users;

namespace DownloadVideo.Service.Mappers;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<UserForCreationDto, User>().ReverseMap();
        CreateMap<UserForResultDto,User>().ReverseMap();
    }
}
