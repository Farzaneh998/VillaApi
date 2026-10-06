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

        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.VillaList);
        }


        [HttpGet("{id}", Name = "GetVilla")]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.VillaList.FirstOrDefault(u => id == id);
            if (villa == null)
                return NotFound();
            else
                return Ok(villa);

        }

        [HttpPost]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (villa == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (VillaStore.VillaList.FirstOrDefault(u => u.Name.ToLower() == villa.Name.ToLower()) != null)
            {
                ModelState.AddModelError("error", "existed!");
                return BadRequest(ModelState);
            }

            villa.Id = VillaStore.VillaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            VillaStore.VillaList.Add(villa);
            return CreatedAtRoute("getVilla", new { id = villa.Id }, villa);
        }


        [HttpDelete("{id}", Name = "DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            if (!VillaStore.VillaList.Any(u => u.Id == id))
            {
                return NotFound();
            }
            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
            VillaStore.VillaList.Remove(villa);
            return NoContent();
        }

        [HttpPut("{id}",Name ="UpdateVilla")]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDto)
        {
            if (villaDto == null)
            {
                return BadRequest();
            }
            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
            villa.Name = villaDto.Name;
            villa.Occupancy = villaDto.Occupancy;
            villa.Sqft = villaDto.Sqft;
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
            var villa= VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
            patchvillaDto.ApplyTo(villa,ModelState);

            return NoContent();
        }
    }
}