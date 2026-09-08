using System.ComponentModel.DataAnnotations;


namespace HotelListing.Api.DTOs.Hotel;


public class UpdateHotelDto
{

    public string? Name { get; set; }

    public string? Address { get; set; }
    
    public double? Rating { get; set; }

    public int CountryId { get; set; }

}