using VillaWebApp.Models;

namespace VillaWebApp.Services.IServices
{
    public interface IBaseService
    {
        
        APIResponse  responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apirequest);
    }
}
