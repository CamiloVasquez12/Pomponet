using Pomponet.Context;
using Pomponet.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Pomponet.Repositories
{
    public interface IPest_X_FungicideRepository
    {
        Task<List<Pest_X_Fungicide>> GetAll();
        Task<Pest_X_Fungicide> GetPest_X_Fungicide(int Id_Pest_X_Fungicide);
        Task<Pest_X_Fungicide> CreatePest_X_Fungicide(int Id_Pest, int Id_Fungicide);
        Task<Pest_X_Fungicide> UpdatePest_X_Fungicide(Pest_X_Fungicide pest_x_fungicide);
        Task<Pest_X_Fungicide> DeletePest_X_Fungicide(Pest_X_Fungicide pest_x_fungicide);
    }
    public class Pest_X_FungicideRepository : IPest_X_FungicideRepository
    {
        private readonly CropsDbContext _db;
        public Pest_X_FungicideRepository(CropsDbContext db)
        {
            _db = db;
        }
        public async Task<Pest_X_Fungicide> CreatePest_X_Fungicide(int id_pest, int id_fungicide)
        {
            Pest_X_Fungicide newPest_X_Fungicide = new Pest_X_Fungicide
            {
                Id_Pest = id_pest,
                Id_Fungicide = id_fungicide,
            };
            await _db.Pest_X_Fungicide.AddAsync(newPest_X_Fungicide);
            _db.SaveChanges();
            return newPest_X_Fungicide;
        }
        public async Task<List<Pest_X_Fungicide>> GetAll()
        {
            return await _db.Pest_X_Fungicide.ToListAsync();
        }
        public async Task<Pest_X_Fungicide> GetPest_X_Fungicide(int Id_Pest_X_Fungicide)
        {
            return await _db.Pest_X_Fungicide.FirstOrDefaultAsync(p => p.Id_Pest_X_Fungicide == Id_Pest_X_Fungicide);
        }
        public async Task<Pest_X_Fungicide> UpdatePest_X_Fungicide(Pest_X_Fungicide pest_x_fungicide)
        {
            Pest_X_Fungicide ConsultUpdate = await _db.Pest_X_Fungicide.FindAsync(pest_x_fungicide.Id_Pest_X_Fungicide);
            if (ConsultUpdate != null)
            {
                ConsultUpdate.Id_Pest = pest_x_fungicide.Id_Pest;
                ConsultUpdate.Id_Fungicide = pest_x_fungicide.Id_Fungicide;
                await _db.SaveChangesAsync();
            }
            return ConsultUpdate;
            throw new NotImplementedException();
        }
        public async Task<Pest_X_Fungicide> DeletePest_X_Fungicide(Pest_X_Fungicide pest_x_fungicide)
        {
            _db.Pest_X_Fungicide.Attach(pest_x_fungicide);
            _db.Entry(pest_x_fungicide).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return pest_x_fungicide;
        }
    }
}
