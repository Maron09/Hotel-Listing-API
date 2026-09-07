using HotelListing.Api.Core.IRepository;
using HotelListing.Api.DTOs.Hotel;
using HotelListing.Api.Models;


namespace HotelListing.Api.Core.IRepository
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<GetCountryDto?> GetCountryWithHotelsAsync(int id);
    }
}