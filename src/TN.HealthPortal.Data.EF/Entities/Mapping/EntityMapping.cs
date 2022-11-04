using AutoMapper;
using TN.HealthPortal.Lib.Models;

namespace TN.HealthPortal.Data.EF.Entities.Mapping
{
    public class EntityMapping : Profile
    {
        public EntityMapping()
        {
            CreateMap<Farm, FarmEntity>().ReverseMap();
            CreateMap<Source, SourceEntity>().ReverseMap();
        }
    }
}
