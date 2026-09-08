namespace HotelListing.Api.Models
{
    public class IdempotencyRecord
    {
        public int Id { get; set; }
        public required string Key { get; set; }
        public required string Response { get; set; }
        public required string Endpoint { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddHours(24);
    }
}