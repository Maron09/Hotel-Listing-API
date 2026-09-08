
namespace HotelListing.Api.DTOs.Hotel
{

    public class CreateCountryDto
    {
        public required string Name { get; set; }

        public required string ShortName { get; set; }
    }


    public class GetCountriesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

    }

    public class GetCountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public List<GetHotelsDto> Hotels { get; set; }
    }
}