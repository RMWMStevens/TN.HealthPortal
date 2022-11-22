using AutoMapper;
using TN.HealthPortal.Logic.DTOs;
using TN.HealthPortal.Logic.Entities;

namespace TN.HealthPortal.API
{
    public class DtoEntityMapping : Profile
    {
        public DtoEntityMapping()
        {
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<CountryDto, Country>().ReverseMap();
            CreateMap<DewormingSchemeDto, DewormingScheme>().ReverseMap();
            CreateMap<DiseaseStatusDto, DiseaseStatus>().ReverseMap();
            CreateMap<FarmDto, Farm>().ReverseMap();
            CreateMap<ManufacturerDto, Manufacturer>().ReverseMap();
            CreateMap<PathogenDto, Pathogen>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<ProductionTypeDto, ProductionType>().ReverseMap();
            CreateMap<RegionDto, Region>().ReverseMap();
            CreateMap<SourceDto, Source>().ReverseMap();
            CreateMap<VeterinarianDto, Veterinarian>().ReverseMap();
        }
    }
}
