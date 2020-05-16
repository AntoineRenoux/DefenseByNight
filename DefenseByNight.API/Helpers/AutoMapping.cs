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
            CreateMap<UserForRegisterViewModel, User>();
            CreateMap<User, UserForRegisterViewModel>();

            CreateMap<User, UserDto>();

            CreateMap<Traduction, TraductionDto>();
            CreateMap<TraductionDto, Traduction>();
            CreateMap<TraductionDto, TraductionViewModel>();
            CreateMap<TraductionViewModel, TraductionDto>();

            CreateMap<Focus, FocusDto>();
            CreateMap<FocusDto, Focus>();

            CreateMap<Power, PowerDto>();
            CreateMap<PowerDto, Power>();
            
            CreateMap<Discipline, DisciplineDto>();
            CreateMap<DisciplineDto, Discipline>();
        }
    }
}