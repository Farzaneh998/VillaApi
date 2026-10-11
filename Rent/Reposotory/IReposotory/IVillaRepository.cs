using Rent.Models;

namespace Rent.Reposotory.IReposotory
{
    public interface IVillaRepository:IRepository<Villa>
    {
        Task<Villa> UpdateAsync(Villa villa);
    }
}
