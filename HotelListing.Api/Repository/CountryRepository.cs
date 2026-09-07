using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Hotel;
using HotelListing.Api.Core.IRepository;
using HotelListing.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly HotelListingDbContext _context;
        public CountryRepository(HotelListingDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<GetCountryDto?> GetCountryWithHotelsAsync(int id)
        {
            return await _context.Countries
                .Where(c => c.CountryId == id)
                .Select(c => new GetCountryDto(
                    c.CountryId,
                    c.Name,
                    c.ShortName,
                    c.Hotels.Select(h => new GetHotelsDto(
                        h.Id,
                        h.Name,
                        h.Address,
                        h.Rating,
                        h.CountryId
                    )).ToList()
                ))
                .FirstOrDefaultAsync();
        }
    }
}