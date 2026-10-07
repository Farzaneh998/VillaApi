using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rent.Data;
using Rent.Models;
using Rent.Models.Dto;

namespace Rent.Controllers
{
    [Route("api/Villa")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            return Ok(await _db.Villas.ToListAsync());
        }


        [HttpGet("{id}", Name = "GetVilla")]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.FirstOrDefaultAsync(u=>u.Id == id);
            if (villa == null)
                return NotFound();
            else
                return Ok(villa);

        }

        [HttpPost]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO villaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (villaDto == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (await _db.Villas.FirstOrDefaultAsync(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("error", "existed!");
                return BadRequest(ModelState);
            }

            Villa model = new Villa()
            {
                Name = villaDto.Name,
                Details = villaDto.Details,
                Occupancy = villaDto.Occupancy,
                Rate = villaDto.Rate,
                ImageUrl = villaDto.ImageUrl,
                Amenity = villaDto.Amenity,
                Sqft = villaDto.Sqft,
            };

            await _db.Villas.AddAsync(model);
            await _db.SaveChangesAsync();
             return CreatedAtRoute("getVilla", new { id = model.Id }, model);
        }


        [HttpDelete("{id}", Name = "DeleteVilla")]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            if (  !_db.Villas.Any(u => u.Id == id))
            {
                return NotFound();
            }
            var villa =await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();
            return NoContent();
        }


        [HttpPut("{id}", Name = "UpdateVilla")]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO villaDto)
        {
            if (villaDto == null)
            {
                return BadRequest();
            }

            Villa villa = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Details = villaDto.Details,
                Occupancy = villaDto.Occupancy,
                Rate = villaDto.Rate,
                ImageUrl = villaDto.ImageUrl,
                Amenity = villaDto.Amenity,
                Sqft = villaDto.Sqft,
            };
            _db.Villas.Update(villa);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        //update one filed
        [HttpPatch("{id}", Name = "Updatepartavilla")]
        public async Task<IActionResult> UpdatePartaVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchvillaDto)
        {
            if (patchvillaDto == null)
            {
                return BadRequest();
            }
            var villa =await _db.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            VillaUpdateDTO villaDto = new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Details = villa.Details,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                ImageUrl = villa.ImageUrl,
                Amenity = villa.Amenity,
                Sqft = villa.Sqft,
            };
            patchvillaDto.ApplyTo(villaDto, ModelState);

            Villa model = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Details = villaDto.Details,
                Occupancy = villaDto.Occupancy,
                Rate = villaDto.Rate,
                ImageUrl = villaDto.ImageUrl,
                Amenity = villaDto.Amenity,
                Sqft = villaDto.Sqft,
            };
            _db.Villas.Update(model);
           await  _db.SaveChangesAsync();
            return NoContent();
        }
    }
}