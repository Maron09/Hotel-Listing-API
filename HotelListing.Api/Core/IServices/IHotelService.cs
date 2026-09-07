using HotelListing.Api.DTOs.Hotel;

namespace HotelListing.Api.Core.IServices
{
    public interface IHotelService
    {
        Task<List<GetHotelsDto>> GetAllAsync();
        Task<GetHotelDto?> GetAsync(int id);
        Task<GetHotelDto> CreateAsync(CreateHotelDto hotelDto);
        Task UpdateAsync(int id, UpdateHotelDto hotelDto);
        Task DeleteAsync(int id);
    }
}