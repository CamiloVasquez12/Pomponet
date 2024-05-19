using Pomponet.Context;
using Pomponet.Model;
using Microsoft.EntityFrameworkCore;

namespace Pomponet.Repositories
{
    public interface IPompo_PartsnRepository
    {
        Task<List<Pompon_Parts>> GetAll();
        Task<Pompon_Parts> GetPompon_Parts(int Id_Pompon_Part);
        Task<Pompon_Parts> CreatePompon_Parts(string Part);
        Task<Pompon_Parts> UpdatePompon_Parts(Pompon_Parts pompon_parts);
        Task<Pompon_Parts> DeletePompon_Parts(Pompon_Parts pompon_parts);
    }
    public class Pompon_PartsRepository : IPompo_PartsnRepository
    {
        private readonly CropsDbContext _db;
        public Pompon_PartsRepository(CropsDbContext db)
        {
            _db = db;
        }
        public async Task<Pompon_Parts> CreatePompon_Parts(string part)
        {
            Pompon_Parts newPompon_Parts = new Pompon_Parts
            {
                Part = part,
            };
            await _db.Pompon_Parts.AddAsync(newPompon_Parts);
            _db.SaveChanges();
            return newPompon_Parts;
        }
        public async Task<List<Pompon_Parts>> GetAll()
        {
            return await _db.Pompon_Parts.ToListAsync();
        }
        public async Task<Pompon_Parts> GetPompon_Parts(int Id_Pompon_Part)
        {
            return await _db.Pompon_Parts.FirstOrDefaultAsync(p => p.Id_Pompon_Part == Id_Pompon_Part);
        }
        public async Task<Pompon_Parts> UpdatePompon_Parts(Pompon_Parts pompon_parts)
        {
            Pompon_Parts ConsultUpdate = await _db.Pompon_Parts.FindAsync(pompon_parts.Id_Pompon_Part);
            if (ConsultUpdate != null)
            {
                ConsultUpdate.Part = pompon_parts.Part;
                await _db.SaveChangesAsync();
            }
            return ConsultUpdate;
            throw new NotImplementedException();
        }
        public async Task<Pompon_Parts> DeletePompon_Parts(Pompon_Parts pompon_parts)
        {
            _db.Pompon_Parts.Attach(pompon_parts);
            _db.Entry(pompon_parts).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return pompon_parts;
        }
    }
}
