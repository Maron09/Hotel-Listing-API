namespace HotelListing.Api.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public IList<Hotel> Hotels { get; set; } = []; // Navigation property to represent the relationship with hotels (One-to-Many relationship with Hotel)
    }
}