using System.ComponentModel.DataAnnotations;


namespace HotelListing.Api.DTOs.Hotel;


public class CreateHotelDto
{

    
    public required string Name { get; set; }


    public required string Address { get; set; }


    public double Rating { get; set; }

    public required int CountryId { get; set; }
}

public record GetHotelsDto(int Id, string Name, string Address, double Rating, int CountryId);
public record GetHotelDto(int Id, string Name, string Address, double Rating, int CountryId, string Country);
