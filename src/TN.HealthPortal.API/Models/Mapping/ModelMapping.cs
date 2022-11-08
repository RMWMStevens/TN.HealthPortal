using AutoMapper;
using TN.HealthPortal.Lib.Entities;

namespace TN.HealthPortal.API.Models.Mapping
{
    public class ModelMapping : Profile
    {
        public ModelMapping()
        {
            CreateMap<FarmCreationRequest, Farm>().ReverseMap();
        }
    }
}
