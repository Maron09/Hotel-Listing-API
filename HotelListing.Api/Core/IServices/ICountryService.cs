using HotelListing.Api.DTOs.Hotel;


namespace HotelListing.Api.Core.IServices
{
    public interface ICountryService
    {
        Task<List<GetCountriesDto>> GetAllAsync();
        Task<GetCountryDto?> GetAsync(int id);
        Task<GetCountryDto> CreateAsync(CreateCountryDto countryDto);
        Task UpdateAsync(int id, UpdateCountryDto countryDto);
        Task DeleteAsync(int id);
    }
}