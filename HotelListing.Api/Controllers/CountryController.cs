using Microsoft.AspNetCore.Mvc;
using HotelListing.Api.DTOs.Hotel;
using HotelListing.Api.Core.IServices;


namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ILogger<CountryController> _logger;

        public CountryController(
            ICountryService countryService,
            ILogger<CountryController> logger
        )
        {
            _countryService = countryService;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountriesDto>>> GetCountries()
        {
            try
            {
                var counties = await _countryService.GetAllAsync();
                return Ok(counties);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Fetching countries");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GetCountryDto>> GetCountry(int id)
        {
            try
            {
                var country = await _countryService.GetAsync(id);
                if (country == null)
                {
                    return NotFound(new { message = $"Country with ID {id} not found" });
                }
                return Ok(country);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error Fetching country with ID: {Id}", id);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<GetCountryDto>> CreateCountry([FromBody] CreateCountryDto countryDto)
        {
            try
            {
                var country = await _countryService.CreateAsync(countryDto);
                return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, country);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating country");
                return StatusCode(500, new { message = "Internal server error" }); 
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDto countryDto)
        {
            try
            {
                await _countryService.UpdateAsync(id, countryDto);
                return NoContent();
            }
            catch (Exception ex) when (ex.Message.Contains("not found"))
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating country with ID: {Id}", id);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            try
            {
                await _countryService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex) when (ex.Message.Contains("not found"))
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting country with ID: {Id}", id);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}