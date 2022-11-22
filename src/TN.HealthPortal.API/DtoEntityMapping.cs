using AutoMapper;
using TN.HealthPortal.Logic.DTOs;
using TN.HealthPortal.Logic.Entities;

namespace TN.HealthPortal.API
{
    public class DtoEntityMapping : Profile
    {
        public DtoEntityMapping()
        {
            CreateMap<FarmDto, Farm>().ReverseMap();
        }
    }
}
