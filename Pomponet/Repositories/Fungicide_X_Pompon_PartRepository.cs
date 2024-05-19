using Pomponet.Context;
using Pomponet.Model;
using Microsoft.EntityFrameworkCore;

namespace Pomponet.Repositories
{
    public interface IFungicide_X_Pompon_PartRepository
    {
        Task<List<Fungicide_X_Pompon_Part>> GetAll();
        Task<Fungicide_X_Pompon_Part> GetFungicide_X_Pompon_Part(int Id_Fungicide_X_Pompon_Part);
        Task<Fungicide_X_Pompon_Part> CreateFungicide_X_Pompon_Part(int Id_Pompon_Part, int Id_Fungicide);
        Task<Fungicide_X_Pompon_Part> UpdateFungicide_X_Pompon_Part(Fungicide_X_Pompon_Part fungicide_x_pompon_part);
        Task<Fungicide_X_Pompon_Part> DeleteFungicide_X_Pompon_Part(Fungicide_X_Pompon_Part fungicide_x_pompon_part);
    }
    public class Fungicide_X_Pompon_PartRepository : IFungicide_X_Pompon_PartRepository
    {
        private readonly CropsDbContext _db;
        public Fungicide_X_Pompon_PartRepository(CropsDbContext db)
        {
            _db = db;
        }
        public async Task<Fungicide_X_Pompon_Part> CreateFungicide_X_Pompon_Part(int id_pompon_part, int id_fungicide)
        {
            Fungicide_X_Pompon_Part newFungicide_X_Pompon_Part = new Fungicide_X_Pompon_Part
            {
                Id_Pompon_Part = id_pompon_part,
                Id_Fungicide = id_fungicide,
            };
            await _db.Fungicide_X_Pompon_Part.AddAsync(newFungicide_X_Pompon_Part);
            _db.SaveChanges();
            return newFungicide_X_Pompon_Part;
        }
        public async Task<List<Fungicide_X_Pompon_Part>> GetAll()
        {
            return await _db.Fungicide_X_Pompon_Part.ToListAsync();
        }
        public async Task<Fungicide_X_Pompon_Part> GetFungicide_X_Pompon_Part(int Id_Fungicide_X_Pompon_Part)
        {
            return await _db.Fungicide_X_Pompon_Part.FirstOrDefaultAsync(f => f.Id_Fungicide_X_Pompon_Part == Id_Fungicide_X_Pompon_Part);
        }
        public async Task<Fungicide_X_Pompon_Part> UpdateFungicide_X_Pompon_Part(Fungicide_X_Pompon_Part fungicide_x_pompon_part)
        {
            Fungicide_X_Pompon_Part ConsultUpdate = await _db.Fungicide_X_Pompon_Part.FindAsync(fungicide_x_pompon_part.Id_Fungicide_X_Pompon_Part);
            if (ConsultUpdate != null)
            {
                ConsultUpdate.Id_Pompon_Part = fungicide_x_pompon_part.Id_Pompon_Part;
                ConsultUpdate.Id_Fungicide = fungicide_x_pompon_part.Id_Fungicide;
                await _db.SaveChangesAsync();
            }
            return ConsultUpdate;
            throw new NotImplementedException();
        }
        public async Task<Fungicide_X_Pompon_Part> DeleteFungicide_X_Pompon_Part(Fungicide_X_Pompon_Part fungicide_x_pompon_part)
        {
            _db.Fungicide_X_Pompon_Part.Attach(fungicide_x_pompon_part);
            _db.Entry(fungicide_x_pompon_part).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return fungicide_x_pompon_part;
        }
    }
}
