using HotelListing.Api.Core.IRepository;
using HotelListing.Api.DTOs.Hotel;
using HotelListing.Api.Models;
using HotelListing.Api.Core.IServices;


namespace HotelListing.Api.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly ILogger<HotelService> _logger;

        public HotelService(
            IHotelRepository hotelRepository,
            ILogger<HotelService> logger
        )
        {
            _hotelRepository = hotelRepository;
            _logger = logger;
        }

        public async Task<List<GetHotelsDto>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all hotes");
            var hotels = await _hotelRepository.GetAllAsync();
            return [.. hotels.Select(h => new GetHotelsDto(
                h.Id,
                h.Name,
                h.Address,
                h.Rating,
                h.CountryId
            ))];
        }

        public async Task<GetHotelDto?> GetAsync(int id)
        {
            _logger.LogInformation("Fetching hotel with ID: {id}", id);
            return await _hotelRepository.GetHotelWithCountryAsync(id);
        }
        public async Task<GetHotelDto> CreateAsync(CreateHotelDto hotelDto)
        {
            _logger.LogInformation("Creating a new Hotel: {Name}", hotelDto.Name);
            var hotel = new Hotel
            {
                Name = hotelDto.Name,
                Address = hotelDto.Address,
                Rating = hotelDto.Rating,
                CountryId = hotelDto.CountryId
            };
            await _hotelRepository.AddAsync(hotel);
            return await _hotelRepository.GetHotelWithCountryAsync(hotel.Id)
                ?? new GetHotelDto(hotel.Id, hotel.Name, hotel.Address, hotel.Rating, hotel.CountryId, string.Empty);
        }

        public async Task UpdateAsync(int id, UpdateHotelDto hotelDto)
        {
            _logger.LogInformation("Updating hotel with ID: {id}", id);
            var hotel = await _hotelRepository.GetAsync(id) ?? throw new Exception($"Hotel with ID {id} not found.");
            hotel.Name = hotelDto.Name ?? hotel.Name;
            hotel.Address = hotelDto.Address ?? hotel.Address;
            hotel.Rating = hotelDto.Rating ?? hotel.Rating;
            hotel.CountryId = hotelDto.CountryId;
            await _hotelRepository.UpdateAsync(hotel);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting hotel with ID: {id}", id);
            var exists = await _hotelRepository.ExistsAsync(id);
            if (!exists)
                throw new Exception($"Hotel with ID {id} not found.");

            await _hotelRepository.DeleteAsync(id);
        }
    }
}
