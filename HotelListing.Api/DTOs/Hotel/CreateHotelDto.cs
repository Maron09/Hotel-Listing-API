using System.ComponentModel.DataAnnotations;


namespace HotelListing.Api.DTOs.Hotel;


public class CreateHotelDto
{

    
    public required string Name { get; set; }


    public required string Address { get; set; }


    public double Rating { get; set; }

    public required int CountryId { get; set; }
}

public class GetHotelsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public double Rating { get; set; }
    public int CountryId { get; set; }
}
public class GetHotelDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public double Rating { get; set; }
    public string Country { get; set; }
}
