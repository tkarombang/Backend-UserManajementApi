using AutoMapper;
using Backend_UserManagementApi.DTOs;
using Backend_UserManagementApi.Models;

namespace Backend_UserManagementApi.Profiles
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<User, UserReadDto>();
      CreateMap<UserCreateDto, User>();
      CreateMap<UserUpdateDto, User>();
      CreateMap<User, UserUpdateDto>();
    }
  }
}