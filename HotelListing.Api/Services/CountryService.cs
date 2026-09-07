using HotelListing.Api.Core.IServices;
using HotelListing.Api.DTOs.Hotel;
using HotelListing.Api.Core.IRepository;
using HotelListing.Api.Models;

namespace HotelListing.Api.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ILogger<CountryService> _logger;

        public CountryService(
            ICountryRepository countryRepository,
            ILogger<CountryService> logger
        )
        {
            _countryRepository = countryRepository;
            _logger = logger;
        }
        
        public async Task<List<GetCountriesDto>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all countries");
            var countries = await _countryRepository.GetAllAsync();
            return [.. countries.Select(c => new GetCountriesDto(
                c.CountryId,
                c.Name,
                c.ShortName
            ))];
        }

        public async Task<GetCountryDto?> GetAsync(int id)
        {
            _logger.LogInformation("Fetching country with ID: {Id}", id);
            return await _countryRepository.GetCountryWithHotelsAsync(id);
        }

        public async Task<GetCountryDto> CreateAsync(CreateCountryDto countryDto)
        {
            _logger.LogInformation("Creating a new Country: {Name}", countryDto.Name);
            var country = new Country
            {
                Name = countryDto.Name,
                ShortName = countryDto.ShortName
            };
            await _countryRepository.AddAsync(country);
            return new GetCountryDto(
                country.CountryId,
                country.Name,
                country.ShortName,
                new List<GetHotelsDto>()
            );
        }

        public async Task UpdateAsync(int id, UpdateCountryDto countryDto)
        {
            _logger.LogInformation($"Updating country with ID: {id}");
            var country = await _countryRepository.GetAsync(id);
            if (country == null)
                throw new Exception($"Country with ID {id} not found.");
            
            country.Name = countryDto.Name ?? country.Name;
            country.ShortName = countryDto.ShortName ?? country.ShortName;
            await _countryRepository.UpdateAsync(country);

        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation($"Deleting country with ID: {id}");
            var exists = await _countryRepository.ExistsAsync(id);
            if (!exists)
                throw new Exception($"Country with ID {id} not found.");
            
            await _countryRepository.DeleteAsync(id);
        }
    }
}