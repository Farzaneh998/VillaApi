using Rent.Data;
using Rent.Models;
using Rent.Reposotory.IReposotory;

namespace Rent.Reposotory
{
    public class VillaRepository :Repository<Villa> ,IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<Villa> UpdateAsync(Villa entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;  
        }

    }
}
