using Microsoft.AspNetCore.Mvc;
using static RentVilla.SD;

namespace VillaWebApp.Models
{
    public class APIRequest
    {

        public ApiType apiType { get; set; } = ApiType.GET;
        public string Url {  get; set; }
        public object Data {  get; set; }
    }
}
