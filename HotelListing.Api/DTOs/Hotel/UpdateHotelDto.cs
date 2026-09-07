using System.ComponentModel.DataAnnotations;


namespace HotelListing.Api.DTOs.Hotel;


public class UpdateHotelDto
{

    [StringLength(100)]
    public string? Name { get; set; }

    [MaxLength(500)]
    public string? Address { get; set; }
    
    [Range(1, 5)]
    public double? Rating { get; set; }

    public int CountryId { get; set; }

}