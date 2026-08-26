using HotelListing.Api.Data;
using Microsoft.AspNetCore.Mvc;


namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private static readonly List<Hotel> hotels = new List<Hotel>
        {
            new() { Id = 1, Name = "Hotel A", Address = "Address A", Rating = 4.5 },
            new() { Id = 2, Name = "Hotel B", Address = "Address B", Rating = 3.8 },
        };

        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> Get()
        {
            return Ok(hotels);
        }

        [HttpGet("{id}")]
        public ActionResult<Hotel> Get(int id)
        {
            var hotel = hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null)
            {
                return NotFound(new { message = $"Hotel with ID {id} not found." });
            }
            return Ok(hotel);
        }

        [HttpPost]
        public ActionResult<Hotel> Post([FromBody] Hotel hotel)
        {
            if(hotels.Any(h => h.Id == hotel.Id))
            {
                return BadRequest("Hotel with the same ID already exists.");
            }
            hotels.Add(hotel);
            return CreatedAtAction(nameof(Get), new { id = hotel.Id }, hotel);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Hotel hotel)
        {
            var existingHotel = hotels.FirstOrDefault(h => h.Id == id);
            if (existingHotel == null)
            {
                return NotFound(new { message = $"Hotel with ID {id} not found." });
            }
            existingHotel.Name = hotel.Name;
            existingHotel.Address = hotel.Address;
            existingHotel.Rating = hotel.Rating;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var hotel = hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }
            hotels.Remove(hotel);
            return NoContent();
        }

        
    }
}

