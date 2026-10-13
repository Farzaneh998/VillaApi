using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VillaWebApp.Models;
using VillaWebApp.Models.Dto;
using VillaWebApp.Services;

namespace VillaWebApp.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;
        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }
        //public async Task<IActionResult> IndexVilla()
        //{
        //    List<VillaDTO> List = new();
        //    var response = _villaService.GetAllAsync<APIResponse>();
        //    if (response != null)
        //    {
        //        List = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
        //    }
        //    Console.WriteLine(List);
        //    return View(List);
        //}
        public async Task<IActionResult> Index()
        {
            List<VillaDTO> List = new();

            var response = await _villaService.GetAllAsync<APIResponse>();

            if (response != null && response.IsSuccess && response.Result != null)
            {
                var jsonString = JsonConvert.SerializeObject(response.Result);

                List = JsonConvert.DeserializeObject<List<VillaDTO>>(jsonString);
            }
            else
            {
                Console.WriteLine("API request failed or returned empty result.");
                if (response != null && response.ErrorMassage != null)
                {
                    Console.WriteLine(string.Join(", ", response.ErrorMassage));
                }
            }
          //  Console.WriteLine(List);
            return View(List);
        }
    }
}
