using Pomponet.Context;
using Pomponet.Model;
using Microsoft.EntityFrameworkCore;

namespace Pomponet.Repositories
{
    public interface IPestsRepository
    {
        Task<List<Pests>> GetAll();
        Task<Pests> GetPests(int Id_Pest);
        Task<Pests> CreatePests(string Pest);
        Task<Pests> UpdatePests(Pests pests);
        Task<Pests> DeletePests(Pests pests);
    }
    public class PestsRepository : IPestsRepository
    {
        private readonly CropsDbContext _db;
        public PestsRepository(CropsDbContext db)
        {
            _db = db;
        }
        public async Task<Pests> CreatePests(string pest)
        {
            Pests newPests = new Pests
            {
                Pest = pest,
            };
            await _db.Pests.AddAsync(newPests);
            _db.SaveChanges();
            return newPests;
        }
        public async Task<List<Pests>> GetAll()
        {
            return await _db.Pests.ToListAsync();
        }
        public async Task<Pests> GetPests(int Id_Pest)
        {
            return await _db.Pests.FirstOrDefaultAsync(p => p.Id_Pest == Id_Pest);
        }
        public async Task<Pests> UpdatePests(Pests pests)
        {
            Pests ConsultUpdate = await _db.Pests.FindAsync(pests.Id_Pest);
            if (ConsultUpdate != null)
            {
                ConsultUpdate.Pest = pests.Pest;
                await _db.SaveChangesAsync();
            }
            return ConsultUpdate;
            throw new NotImplementedException();
        }
        public async Task<Pests> DeletePests(Pests pests)
        {
            _db.Pests.Attach(pests);
            _db.Entry(pests).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return pests;
        }
    }
}
