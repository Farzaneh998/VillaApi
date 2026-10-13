using System.Net;

namespace VillaWebApp.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public object Result {  get; set; }
        public List<string> ErrorMassage { get; set; }
    }
}
