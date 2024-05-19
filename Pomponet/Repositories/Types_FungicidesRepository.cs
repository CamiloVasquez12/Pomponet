using Pomponet.Context;
using Pomponet.Model;
using Microsoft.EntityFrameworkCore;

namespace Pomponet.Repositories
{
    public interface ITypes_FungicidesRepository
    {
        Task<List<Types_Fungicides>> GetAll();
        Task<Types_Fungicides> GetTypes_Fungicides(int Id_Type_Fungicide);
        Task<Types_Fungicides> CreateTypes_Fungicides(string Type_Fungicide, int Id_Funicides);
        Task<Types_Fungicides> UpdateTypes_Fungicides(Types_Fungicides types_fungicides);
        Task<Types_Fungicides> DeleteTypes_Fungicides(Types_Fungicides types_fungicides);
    }
    public class Types_FungicidesRepository : ITypes_FungicidesRepository
    {
        private readonly CropsDbContext _db;
        public Types_FungicidesRepository(CropsDbContext db)
        {
            _db = db;
        }
        public async Task<Types_Fungicides> CreateTypes_Fungicides(string type_fungicide, int id_funicides)
        {
            Types_Fungicides newTypes_Fungicides = new Types_Fungicides
            {
                Type_Fungicide = type_fungicide,
                Id_Funicides = id_funicides,
            };
            await _db.Types_Fungicides.AddAsync(newTypes_Fungicides);
            _db.SaveChanges();
            return newTypes_Fungicides;
        }
        public async Task<List<Types_Fungicides>> GetAll()
        {
            return await _db.Types_Fungicides.ToListAsync();
        }
        public async Task<Types_Fungicides> GetTypes_Fungicides(int Id_Type_Fungicide)
        {
            return await _db.Types_Fungicides.FirstOrDefaultAsync(t => t.Id_Type_Fungicide == Id_Type_Fungicide);
        }
        public async Task<Types_Fungicides> UpdateTypes_Fungicides(Types_Fungicides types_fungicides)
        {
            Types_Fungicides ConsultUpdate = await _db.Types_Fungicides.FindAsync(types_fungicides.Id_Type_Fungicide);
            if (ConsultUpdate != null)
            {
                ConsultUpdate.Type_Fungicide = types_fungicides.Type_Fungicide;
                ConsultUpdate.Id_Funicides = types_fungicides.Id_Funicides;
                await _db.SaveChangesAsync();
            }
            return ConsultUpdate;
            throw new NotImplementedException();
        }
        public async Task<Types_Fungicides> DeleteTypes_Fungicides(Types_Fungicides types_fungicides)
        {
            _db.Types_Fungicides.Attach(types_fungicides);
            _db.Entry(types_fungicides).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return types_fungicides;
        }
    }
}
