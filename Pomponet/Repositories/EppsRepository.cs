using Pomponet.Context;
using Pomponet.Model;
using Microsoft.EntityFrameworkCore;

namespace Pomponet.Repositories
{
    public interface IEppsRepository
    {
        Task<List<Epps>> GetAll();
        Task<Epps> GetEpps(int Id_Epp);
        Task<Epps> CreateEpps(string Name_Epp);
        Task<Epps> UpdateEpps(Epps epps);
        Task<Epps> DeleteEpps(Epps epps);
    }
    public class EppsRepository : IEppsRepository
    {
        private readonly CropsDbContext _db;
        public EppsRepository(CropsDbContext db)
        {
            _db = db;
        }
        public async Task<Epps> CreateEpps(string name_epp)
        {
            Epps newEpps = new Epps
            {
                Name_Epp = name_epp,
            };
            await _db.Epps.AddAsync(newEpps);
            _db.SaveChanges();
            return newEpps;
        }
        public async Task<List<Epps>> GetAll()
        {
            return await _db.Epps.ToListAsync();
        }
        public async Task<Epps> GetEpps(int Id_Epp)
        {
            return await _db.Epps.FirstOrDefaultAsync(e => e.Id_Epp == Id_Epp);
        }
        public async Task<Epps> UpdateEpps(Epps epps)
        {
            Epps ConsultUpdate = await _db.Epps.FindAsync(epps.Id_Epp);
            if (ConsultUpdate != null)
            {
                ConsultUpdate.Name_Epp = epps.Name_Epp;
                await _db.SaveChangesAsync();
            }
            return ConsultUpdate;
            throw new NotImplementedException();
        }
        public async Task<Epps> DeleteEpps(Epps epps)
        {
            _db.Epps.Attach(epps);
            _db.Entry(epps).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return epps;
        }
    }
}
