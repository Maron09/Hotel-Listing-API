using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Hotel;
using HotelListing.Api.Core.IRepository;
using HotelListing.Api.Models;
using Microsoft.EntityFrameworkCore;


namespace HotelListing.Api.Repository
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        private readonly HotelListingDbContext _context;

        public HotelRepository(HotelListingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<GetHotelDto?> GetHotelWithCountryAsync(int id)
        {
            return await _context.Hotels
                .Where(h => h.Id == id)
                .Select(h => new GetHotelDto(
                    h.Id,
                    h.Name,
                    h.Address,
                    h.Rating,
                    h.CountryId,
                    h.Country != null ? h.Country.Name : "Unknown"
                ))
                .FirstOrDefaultAsync();
        }
    }
}