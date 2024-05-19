using Pomponet.Context;
using Pomponet.Model;
using Microsoft.EntityFrameworkCore;

namespace Pomponet.Repositories
{
    public interface IFungicidesRepository
    {
        Task<List<Fungicides>> GetAll();
        Task<Fungicides> GetFungicides(int Id_Fungicide);
        Task<Fungicides> CreateFungicides(string Name_Fungicide, int Quantity, string Description, int Id_crop);
        Task<Fungicides> UpdateFungicides(Fungicides fungicides);
        Task<Fungicides> DeleteFungicides(Fungicides fungicides);
    }
    public class FungicidesRepository : IFungicidesRepository
    {
        private readonly CropsDbContext _db;
        public FungicidesRepository(CropsDbContext db)
        {
            _db = db;
        }
        public async Task<Fungicides> CreateFungicides(string name_fungicide, int quantity, string description, int id_crop)
        {
            Fungicides newFungicides = new Fungicides
            {
                Name_Fungicide = name_fungicide,
                Quantity = quantity,
                Description = description,
                Id_crop = id_crop,
            };
            await _db.Fungicides.AddAsync(newFungicides);
            _db.SaveChanges();
            return newFungicides;
        }
        public async Task<List<Fungicides>> GetAll()
        {
            return await _db.Fungicides.ToListAsync();
        }
        public async Task<Fungicides> GetFungicides(int Id_Fungicide)
        {
            return await _db.Fungicides.FirstOrDefaultAsync(f => f.Id_Fungicide == Id_Fungicide);
        }
        public async Task<Fungicides> UpdateFungicides(Fungicides fungicides)
        {
            Fungicides ConsultUpdate = await _db.Fungicides.FindAsync(fungicides.Id_Fungicide);
            if (ConsultUpdate != null)
            {
                ConsultUpdate.Name_Fungicide = fungicides.Name_Fungicide;
                ConsultUpdate.Quantity = fungicides.Quantity;
                ConsultUpdate.Description = fungicides.Description;
                ConsultUpdate.Id_crop = fungicides.Id_crop;
                await _db.SaveChangesAsync();
            }
            return ConsultUpdate;
            throw new NotImplementedException();
        }
        public async Task<Fungicides> DeleteFungicides(Fungicides fungicides)
        {
            _db.Fungicides.Attach(fungicides);
            _db.Entry(fungicides).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return fungicides;
        }
    }
}
