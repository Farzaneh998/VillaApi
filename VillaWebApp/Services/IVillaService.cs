using VillaWebApp.Models.Dto;

namespace VillaWebApp.Services
{
    public interface IVillaService
    {

        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(VillaCreateDTO dto);
        Task<T> UpdateAsync <T>(VillaUpdateDTO dto);
        Task DeleteAsync<T>(int id);    

    }
}
