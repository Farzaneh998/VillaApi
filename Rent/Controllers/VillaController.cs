using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rent.Data;
using Rent.Models;
using Rent.Models.Dto;
using Rent.Reposotory.IReposotory;
using System.Net;

namespace Rent.Controllers
{
    [Route("api/Villa")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly IVillaRepository _VillaRepository;
        private readonly IMapper _mapper;//profile :imapper
        protected APIResponse _reponse;

        public VillaController(IVillaRepository villaRepository, IMapper mapper)
        {
            _VillaRepository = villaRepository;
            _mapper = mapper;
            this._reponse = new();
        }


        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                IEnumerable<Villa> villalist = await _VillaRepository.GetAllAsync();

                _reponse.StatusCode = HttpStatusCode.OK;
                _reponse.Result = _mapper.Map<List<VillaDTO>>(villalist);
                return Ok(_reponse);
            }

            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.ErrorMassage = new List<string> { ex.Message };
            }
            return (_reponse);

        }


        [HttpGet("{id}", Name = "GetVilla")]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _reponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_reponse);
                }
                var villa = await _VillaRepository.GetAsync(u => u.Id == id);
                if (villa == null)
                {
                    _reponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_reponse);
                }
                else

                    _reponse.StatusCode = HttpStatusCode.OK;
                _reponse.Result = _mapper.Map<VillaDTO>(villa);
                return Ok(_reponse);
            }

            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.ErrorMassage = new List<string> { ex.Message };
            }
            return (_reponse);

        }

        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDTO villaDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _reponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_reponse);
                }
                if (villaDto == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                if (await _VillaRepository.GetAsync(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)
                {
                    _reponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_reponse);
                    //    ModelState.AddModelError("error", "existed!");
                    //    return BadRequest(ModelState);
                }
                Villa model = _mapper.Map<Villa>(villaDto);

                await _VillaRepository.CreateAsync(model);

                _reponse.StatusCode = HttpStatusCode.Created;
                _reponse.Result = _mapper.Map<VillaDTO>(model);

                return CreatedAtRoute("getVilla", new { id = model.Id }, model);
            }

            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.ErrorMassage = new List<string> { ex.Message };
            }
            return (_reponse);
        }


        [HttpDelete("{id}", Name = "DeleteVilla")]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _reponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_reponse);
                }
                //if (!_db.Villas.Any(u => u.Id == id))
                //{
                //    return NotFound();
                //}
                var villa = await _VillaRepository.GetAsync(u => u.Id == id);
                await _VillaRepository.DeleteAsync(villa);

                _reponse.StatusCode = HttpStatusCode.NoContent;
                return Ok(_reponse);
            }

            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.ErrorMassage = new List<string> { ex.Message };
            }
            return Ok(_reponse);
        }


        [HttpPut("{id}", Name = "UpdateVilla")]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDTO villaDto)
        {
            try
            {
                if (villaDto == null)
                {
                    _reponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_reponse);
                }
                Villa villa = _mapper.Map<Villa>(villaDto);
                await _VillaRepository.UpdateAsync(villa);
                _reponse.StatusCode = HttpStatusCode.NoContent;

                return Ok(_reponse);
            }

            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.ErrorMassage = new List<string> { ex.Message };
            }
            return (_reponse);
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