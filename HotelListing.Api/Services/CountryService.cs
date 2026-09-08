using HotelListing.Api.Core.IServices;
using HotelListing.Api.DTOs.Hotel;
using HotelListing.Api.Core.IRepository;
using HotelListing.Api.Models;
using AutoMapper;

namespace HotelListing.Api.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ILogger<CountryService> _logger;
        private readonly IMapper _mapper;

        public CountryService(
            ICountryRepository countryRepository,
            ILogger<CountryService> logger,
            IMapper mapper
        )
        {
            _countryRepository = countryRepository;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<List<GetCountriesDto>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all countries");
            var countries = await _countryRepository.GetAllAsync();
            return _mapper.Map<List<GetCountriesDto>>(countries);
        }

        public async Task<GetCountryDto?> GetAsync(int id)
        {
            _logger.LogInformation("Fetching country with ID: {Id}", id);
            return await _countryRepository.GetCountryWithHotelsAsync(id);
        }

        public async Task<GetCountryDto> CreateAsync(CreateCountryDto countryDto)
        {
            _logger.LogInformation("Creating a new Country: {Name}", countryDto.Name);
            var country = _mapper.Map<Country>(countryDto);
            await _countryRepository.AddAsync(country);
            return _mapper.Map<GetCountryDto>(country);
        }

        public async Task UpdateAsync(int id, UpdateCountryDto countryDto)
        {
            _logger.LogInformation("Updating country with ID: {id}", id);
            var country = await _countryRepository.GetAsync(id) ?? throw new Exception($"Country with ID {id} not found.");
            country.Name = countryDto.Name ?? country.Name;
            country.ShortName = countryDto.ShortName ?? country.ShortName;
            await _countryRepository.UpdateAsync(country);

        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting country with ID: {id}", id);
            var exists = await _countryRepository.ExistsAsync(id);
            if (!exists)
                throw new Exception($"Country with ID {id} not found.");
            
            await _countryRepository.DeleteAsync(id);
        }
    }
}