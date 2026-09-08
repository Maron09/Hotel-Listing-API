namespace HotelListing.Api.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public double Rating { get; set; }
        public int CountryId { get; set; } // Foreign key to Country
        public Country? Country { get; set; } // Navigation property to represent the relationship with Country
    }


    
}