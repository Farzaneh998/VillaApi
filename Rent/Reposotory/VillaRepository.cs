using Rent.Data;
using Rent.Models;
using Rent.Reposotory.IReposotory;

namespace Rent.Reposotory
{
    public class VillaNumberRepository :Repository<VillaNumber> ,IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;  
        }

    }
}
