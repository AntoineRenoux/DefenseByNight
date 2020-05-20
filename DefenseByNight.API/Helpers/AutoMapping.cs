using AutoMapper;
using DefenseByNight.API.Data.Entities;
using DefenseByNight.API.Data.Identities;
using DefenseByNight.API.Dtos;
using DefenseByNight.API.Models;

namespace DefenseByNight.API.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<UserForRegisterViewModel, UserRegisterDto>();
            CreateMap<UserRegisterDto, UserForRegisterViewModel>();

            CreateMap<User, UserRegisterDto>();
            CreateMap<UserRegisterDto, User>();

            CreateMap<UserForRegisterViewModel, UserDto>();
            CreateMap<UserDto, UserForRegisterViewModel>();
            
            CreateMap<UserDto, UserRegisterDto>();
            CreateMap<UserRegisterDto, UserDto>();

            CreateMap<UserForLoginViewModel, UserLoginDto>();
            CreateMap<UserLoginDto, UserForLoginViewModel>();

            CreateMap<User, UserLoginDto>();
            CreateMap<UserLoginDto, User>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Focus, FocusDto>();
            CreateMap<FocusDto, Focus>();

            CreateMap<Power, PowerDto>();
            CreateMap<PowerDto, Power>();

            CreateMap<Discipline, DisciplineDto>();
            CreateMap<DisciplineDto, Discipline>();
        }
    }
}