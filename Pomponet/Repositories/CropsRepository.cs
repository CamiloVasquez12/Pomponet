using Pomponet.Context;
using Pomponet.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Pomponet.Repositories
{
    public interface ICropsRepository
    {
        Task<List<Crops>> GetAll();
        Task<Crops> GetCrops(int Id_Crop);
        Task<Crops> CreateCrops(int Crop_Number, int Id_Player);
        Task<Crops> UpdateCrops(Crops crops);
        Task<Crops> DeleteCrops(Crops crops);
    }
    public class CropsRepository : ICropsRepository
    {
        private readonly CropsDbContext _db;
        public CropsRepository(CropsDbContext db)
        {
            _db = db;
        }
        public async Task<Crops> CreateCrops(int crop_number, int id_player)
        {
            Crops newCrops = new Crops
            {
                Crop_Number = crop_number,
                Id_Player = id_player,
            };
            await _db.Crop.AddAsync(newCrops);
            _db.SaveChanges();
            return newCrops;
        }
        public async Task<List<Crops>> GetAll()
        {
            return await _db.Crop.ToListAsync();
        }
        public async Task<Crops> GetCrops(int Id_Crop)
        {
            return await _db.Crop.FirstOrDefaultAsync(c => c.Id_Crop == Id_Crop);
        }
        public async Task<Crops> UpdateCrops(Crops crops)
        {
            Crops ConsultUpdate = await _db.Crop.FindAsync(crops.Id_Crop);
            if (ConsultUpdate != null)
            {
                //?? ConsultUpdate.Id_Crop = id_crop;
                ConsultUpdate.Crop_Number = crops.Crop_Number;
                ConsultUpdate.Id_Player = crops.Id_Player;

                await _db.SaveChangesAsync();
            }
            return ConsultUpdate;
            throw new NotImplementedException();
        }
        public async Task<Crops> DeleteCrops(Crops crops)
        {
            _db.Crop.Attach(crops);
            _db.Entry(crops).State = EntityState.Modified; 
            await _db.SaveChangesAsync();
            return crops;
        }
    }
}
