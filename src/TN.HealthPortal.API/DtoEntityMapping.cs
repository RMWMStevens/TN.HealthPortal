using AutoMapper;
using TN.HealthPortal.Logic.DTOs;
using TN.HealthPortal.Logic.Entities;

namespace TN.HealthPortal.API
{
    public class ModelMapping : Profile
    {
        public ModelMapping()
        {
            CreateMap<FarmDto, Farm>().ReverseMap();
        }
    }
}
