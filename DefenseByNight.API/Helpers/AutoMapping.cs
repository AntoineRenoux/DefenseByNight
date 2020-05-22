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
            #region User
            CreateMap<UserForRegisterViewModel, UserRegisterDto>();
            CreateMap<UserRegisterDto, UserForRegisterViewModel>();

            CreateMap<User, UserRegisterDto>();
            CreateMap<UserRegisterDto, User>();

            CreateMap<UserForRegisterViewModel, UserDto>();
            CreateMap<UserDto, UserForRegisterViewModel>();

            CreateMap<UserDto, UserRegisterDto>();
            CreateMap<UserRegisterDto, UserDto>();

            CreateMap<UserViewModel, UserDto>();
            CreateMap<UserDto, UserViewModel>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<UserForLoginViewModel, UserLoginDto>();
            CreateMap<UserLoginDto, UserForLoginViewModel>();

            CreateMap<User, UserLoginDto>();
            CreateMap<UserLoginDto, User>();

            CreateMap<UserViewModel, UserDto>();
            CreateMap<UserDto, UserViewModel>();
            #endregion

            #region Photo
            CreateMap<Photo, PhotoDto>();
            CreateMap<PhotoDto, Photo>();

            CreateMap<PhotoForCreationViewModel, PhotoDto>();
            CreateMap<PhotoDto, PhotoForCreationViewModel>();

            CreateMap<PhotoViewModel, PhotoDto>();
            CreateMap<PhotoDto, PhotoViewModel>();
            
            #endregion

            CreateMap<Focus, FocusDto>();
            CreateMap<FocusDto, Focus>();

            CreateMap<Power, PowerDto>();
            CreateMap<PowerDto, Power>();

            CreateMap<Discipline, DisciplineDto>();
            CreateMap<DisciplineDto, Discipline>();


        }
    }
}