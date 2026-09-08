using System.ComponentModel.DataAnnotations;


namespace HotelListing.Api.DTOs.Hotel
{

    public class CreateCountryDto
    {
        public required string Name { get; set; }

        public required string ShortName { get; set; }
    }


    public record GetCountriesDto(
        int Id,
        string Name,
        string ShortName
    );

    public record GetCountryDto(
        int Id,
        string Name,
        string ShortName,
        List<GetHotelsDto> Hotels
    );
}