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
            var country = await _context.Countries
                .Include(c => c.Hotels)
                .FirstOrDefaultAsync(c => c.CountryId == id);
            
            if (country == null) return null;

            return new GetCountryDto
            {
                Id = country.CountryId,
                Name = country.Name,
                ShortName = country.ShortName,
                Hotels = [.. country.Hotels.Select(h => new GetHotelsDto
                {
                    Id = h.Id,
                    Name = h.Name,
                    Address = h.Address,
                    Rating = h.Rating,
                    CountryId = h.CountryId
                })]
            };
        }
    }
}