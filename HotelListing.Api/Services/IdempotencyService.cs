using HotelListing.Api.Core.IServices;
using HotelListing.Api.Models;
using Microsoft.EntityFrameworkCore;
using HotelListing.Api.Data;

namespace HotelListing.Api.Service
{
    public  class IdempotencyService : IIdempotencyService
    {
        private readonly HotelListingDbContext _context;
        private readonly ILogger<IdempotencyService> _logger;

        public IdempotencyService(
            HotelListingDbContext context,
            ILogger<IdempotencyService> logger
        )
        {
            _context = context;
            _logger = logger;
        }

        public async Task<string?> GetCachedResponseAsync(string key)
        {
            _logger.LogInformation("Checking idempontency key: {Key}", key);
            var record = await _context.IdempotencyRecords
                .FirstOrDefaultAsync(r => r.Key == key && r.ExpiresAt > DateTime.UtcNow);

            if (record != null)
                _logger.LogInformation("Idempotency key found - returning cached response.");
            return record?.Response;
        }

        public async Task StoreResponseAsync(string key, string endpoint, string response)
        {
            _logger.LogInformation("Storing response for idempotency key: {Key}", key);
            _context.IdempotencyRecords.Add(new IdempotencyRecord
            {
                Key = key,
                Endpoint = endpoint,
                Response = response,
            });
            await _context.SaveChangesAsync();
        }
    }
}