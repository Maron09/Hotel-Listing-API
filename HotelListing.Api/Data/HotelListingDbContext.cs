using Microsoft.EntityFrameworkCore;
using HotelListing.Api.Models;

namespace HotelListing.Api.Data
{
    public class HotelListingDbContext : DbContext
    {
        public HotelListingDbContext(DbContextOptions<HotelListingDbContext> options) : base(options)
        {
            
        }

        public DbSet<Country> Countries { get; set;}
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<IdempotencyRecord> IdempotencyRecords { get; set; }
    }
}