using HotelListing.Api.Core.IRepository;
using HotelListing.Api.DTOs.Hotel;
using HotelListing.Api.Models;
using HotelListing.Api.Core.IServices;
using AutoMapper;


namespace HotelListing.Api.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly ILogger<HotelService> _logger;
        private readonly IMapper _mapper;

        public HotelService(
            IHotelRepository hotelRepository,
            ILogger<HotelService> logger,
            IMapper mapper
        )
        {
            _hotelRepository = hotelRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<GetHotelsDto>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all hotes");
            var hotels = await _hotelRepository.GetAllAsync();
            return _mapper.Map<List<GetHotelsDto>>(hotels);
        }

        public async Task<GetHotelDto?> GetAsync(int id)
        {
            _logger.LogInformation("Fetching hotel with ID: {id}", id);
            return await _hotelRepository.GetHotelWithCountryAsync(id);
        }
        public async Task<GetHotelDto> CreateAsync(CreateHotelDto hotelDto)
        {
            _logger.LogInformation("Creating a new Hotel: {Name}", hotelDto.Name);
            var hotel = _mapper.Map<Hotel>(hotelDto);
            await _hotelRepository.AddAsync(hotel);
            return await _hotelRepository.GetHotelWithCountryAsync(hotel.Id)
                ?? _mapper.Map<GetHotelDto>(hotel);
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
