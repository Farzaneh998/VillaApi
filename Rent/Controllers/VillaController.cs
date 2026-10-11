using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rent.Data;
using Rent.Models;
using Rent.Models.Dto;
using Rent.Reposotory.IReposotory;

namespace Rent.Controllers
{
    [Route("api/Villa")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly IVillaRepository _VillaRepository;
        private readonly IMapper _mapper;//profile :imapper

        public VillaController(IVillaRepository villaRepository, IMapper mapper)
        {
            _VillaRepository = villaRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            IEnumerable<Villa> villalist = await _VillaRepository.GetAllAsync();
            return Ok(_mapper.Map<List<VillaDTO>>(villalist));
        }


        [HttpGet("{id}", Name = "GetVilla")]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _VillaRepository.GetAsync(u => u.Id == id);
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
            if (await _VillaRepository.GetAsync(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("error", "existed!");
                return BadRequest(ModelState);
            }
            Villa model = _mapper.Map<Villa>(villaDto);

            await _VillaRepository.CreateAsync(model);
            return CreatedAtRoute("getVilla", new { id = model.Id }, model);
        }


        [HttpDelete("{id}", Name = "DeleteVilla")]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            //if (!_db.Villas.Any(u => u.Id == id))
            //{
            //    return NotFound();
            //}
            var villa = await _VillaRepository.GetAsync(u => u.Id == id);
            await _VillaRepository.DeleteAsync(villa);
            return NoContent();
        }


        [HttpPut("{id}", Name = "UpdateVilla")]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO villaDto)
        {
            if (villaDto == null)
            {
                return BadRequest();
            }
            Villa villa = _mapper.Map<Villa>(villaDto);
            await _VillaRepository.UpdateAsync(villa);
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
            var villa = await _VillaRepository.GetAsync(u => u.Id == id, false);
            VillaUpdateDTO villaDto = _mapper.Map<VillaUpdateDTO>(villa);
            patchvillaDto.ApplyTo(villaDto, ModelState);

            Villa model = _mapper.Map<Villa>(villaDto);

            await _VillaRepository.UpdateAsync(villa);
            return NoContent();
        }
    }
}