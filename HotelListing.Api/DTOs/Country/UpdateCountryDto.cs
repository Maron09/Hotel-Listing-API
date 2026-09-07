using System.ComponentModel.DataAnnotations;


namespace HotelListing.Api.DTOs.Hotel;


public class UpdateCountryDto
{
    [StringLength(100)]
    public string? Name { get; set; }

    [MaxLength(3)]
    public string? ShortName { get; set; }
}