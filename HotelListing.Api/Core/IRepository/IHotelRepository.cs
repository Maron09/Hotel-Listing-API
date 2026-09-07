using HotelListing.Api.Core.IRepository;
using HotelListing.Api.DTOs.Hotel;
using HotelListing.Api.Models;


namespace HotelListing.Api.Core.IRepository
{
    public interface IHotelRepository : IGenericRepository<Hotel>
    {
        Task<GetHotelDto?> GetHotelWithCountryAsync(int id);
    }
}