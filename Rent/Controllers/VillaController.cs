using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(_db.Villas.ToList());
        }


        [HttpGet("{id}", Name = "GetVilla")]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => id == id);
            if (villa == null)
                return NotFound();
            else
                return Ok(villa);

        }

        [HttpPost]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (villaDto == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (_db.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("error", "existed!");
                return BadRequest(ModelState);
            }

            villaDto.Id = _db.Villas.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            Villa model = new Villa()
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

            _db.Villas.Add(model);
            _db.SaveChanges();
;            return CreatedAtRoute("getVilla", new { id = villaDto.Id }, villaDto);
        }


        [HttpDelete("{id}", Name = "DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            if (!_db.Villas.Any(u => u.Id == id))
            {
                return NotFound();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            _db.Villas.Remove(villa);
            return NoContent();
        }


        [HttpPut("{id}",Name ="UpdateVilla")]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDto)
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
            _db.SaveChanges();
            return NoContent();
        }

        //update one filed
        [HttpPatch("{id}",Name ="Updatepartavilla")]
        public IActionResult UpdatePartaVilla(int id,JsonPatchDocument<VillaDTO> patchvillaDto)
        {
            if (patchvillaDto == null)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            VillaDTO villaDto = new()
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
            _db.SaveChanges();
            return NoContent();
        }
    }
}