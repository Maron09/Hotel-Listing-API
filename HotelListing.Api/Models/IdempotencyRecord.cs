namespace HotelListing.Api.Models
{
    public class IdempotencyRecord
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Response { get; set; }
        public string Endpoint { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddHours(24);
    }
}