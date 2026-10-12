using Rent.Models;

namespace Rent.Reposotory.IReposotory
{
    public interface IVillaNumberRepository:IRepository<VillaNumber>
    {
        Task<VillaNumber> UpdateAsync(VillaNumber villaNumber);
    }
}
