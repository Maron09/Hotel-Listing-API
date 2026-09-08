using AutoMapper;
using HotelListing.Api.DTOs.Hotel;
using HotelListing.Api.Models;

namespace HotelListing.Api.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // Country mappings
            CreateMap<Country, GetCountriesDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CountryId));

            CreateMap<Country, GetCountryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CountryId));

            CreateMap<CreateCountryDto, Country>();
            CreateMap<UpdateCountryDto, Country>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Hotel mappings
            CreateMap<Hotel, GetHotelsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<Hotel, GetHotelDto>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src =>
                    src.Country != null ? src.Country.Name : "Unknown"));

            CreateMap<CreateHotelDto, Hotel>();
            CreateMap<UpdateHotelDto, Hotel>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}