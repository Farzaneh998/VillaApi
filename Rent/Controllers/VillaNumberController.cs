using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rent.Models;
using Rent.Models.Dto;
using Rent.Reposotory;
using Rent.Reposotory.IReposotory;
using System.Net;

namespace Rent.Controllers
{
    [Route("api/VillaNumber")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
        private readonly IVillaNumberRepository _VillaNumberRepository;
        private readonly IVillaRepository _VillaRepository;
        private readonly IMapper _mapper;
        protected APIResponse _reponse;

        public VillaNumberController(IVillaNumberRepository villaNumberRepository, IMapper mapper, IVillaRepository villaRepository )
        {
            _VillaNumberRepository = villaNumberRepository;
            _mapper = mapper;
            this._reponse = new();
            _VillaRepository = villaRepository;
        }


        [HttpGet(Name = "GetAll")]
        public async Task<ActionResult<APIResponse>> GetVillaNumbers()
        {
            try
            {
                IEnumerable<VillaNumber> villaNumberlist = await _VillaNumberRepository.GetAllAsync();
                _reponse.StatusCode = HttpStatusCode.OK;
                _reponse.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumberlist);
                return Ok(_reponse);
            }
            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.ErrorMassage = new List<string> { ex.Message };
            }
            return (_reponse);
        }

        [HttpGet("{Number}", Name = "GetVillaNumber")]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int Number)
        {
            try
            {
                if (Number == 0)
                {
                    _reponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_reponse);
                }
                var villaNumber = await _VillaNumberRepository.GetAsync(u => u.VillaNo == Number);
                if (villaNumber == null)
                {
                    _reponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_reponse);
                }
                else

                    _reponse.StatusCode = HttpStatusCode.OK;
                _reponse.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
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
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO villaNumbercreateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _reponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_reponse);
                }
                if (villaNumbercreateDto == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                if (await _VillaNumberRepository.GetAsync(u => u.VillaNo == villaNumbercreateDto.VillaNo)!= null)
                {
                    ModelState.AddModelError("error", "existed!");
                    _reponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_reponse);   
                }

                if (await _VillaRepository.GetAsync(u => u.Id == villaNumbercreateDto.VillaId) == null)
                {
                    ModelState.AddModelError("error", "villaId is invalid!");
                    _reponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_reponse);
                }

                VillaNumber model = _mapper.Map<VillaNumber>(villaNumbercreateDto);

                await _VillaNumberRepository.CreateAsync(model);

                _reponse.StatusCode = HttpStatusCode.Created;
                _reponse.Result = _mapper.Map<VillaNumberDTO>(model);

                return CreatedAtRoute("GetAll", new { id = model.VillaNo }, model);
            }

            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.ErrorMassage = new List<string> { ex.Message };
            }
            return (_reponse);
        }


        [HttpDelete("{Number}", Name = "DeleteVillaNumber")]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int Number)
        {
            try
            {
                if (Number == 0)
                {
                    _reponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_reponse);
                }
   
                var villaNumber = await _VillaNumberRepository.GetAsync(u => u.VillaNo == Number);
                await _VillaNumberRepository.DeleteAsync(villaNumber);

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


        [HttpPut("{id}", Name = "UpdateVillaNumber")]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDTO villaNumberUpdateDto)
        {
            try
            {
                if (villaNumberUpdateDto == null)
                {
                    _reponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_reponse);
                }

                if (await _VillaRepository.GetAsync(u => u.Id == villaNumberUpdateDto.VillaId) == null)
                {
                    ModelState.AddModelError("error", "villaId is invalid!");
                    _reponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_reponse);
                }

                VillaNumber villaNumber = _mapper.Map<VillaNumber>(villaNumberUpdateDto);
                await _VillaNumberRepository.UpdateAsync(villaNumber);
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


    }
}
