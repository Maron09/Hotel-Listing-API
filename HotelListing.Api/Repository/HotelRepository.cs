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
            var hotel = await _context.Hotels
                .Include(h => h.Country)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null) return null;

            return new GetHotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Address = hotel.Address,
                Rating = hotel.Rating,
                Country = hotel.Country != null ? hotel.Country.Name : string.Empty
            };
        }
    }
}