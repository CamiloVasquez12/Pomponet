using Pomponet.Context;
using Pomponet.Model;
using Microsoft.EntityFrameworkCore;

namespace Pomponet.Repositories
{
    public interface IInventoriesRepository
    {
        Task<List<Inventories>> GetAll();
        Task<Inventories> GetInventories(int Id_Inventory);
        Task<Inventories> CreateInventories(int Number_Inventorie, int Id_Person, int Id_Tool, int Id_Epp);
        Task<Inventories> UpdateInventories(Inventories inventories);
        Task<Inventories> DeleteInventories(Inventories inventories);
    }
    public class InventoriesRepository : IInventoriesRepository
    {
        private readonly CropsDbContext _db;
        public InventoriesRepository(CropsDbContext db)
        {
            _db = db;
        }
        public async Task<Inventories> CreateInventories(int number_inventorie, int id_person, int id_tool, int id_epp)
        {
            Inventories newInventories = new Inventories
            {
                Number_Inventorie = number_inventorie,
                Id_Person = id_person,
                Id_Tool = id_tool,
                Id_Epp = id_epp,
            };
            await _db.Inventories.AddAsync(newInventories);
            _db.SaveChanges();
            return newInventories;
        }
        public async Task<List<Inventories>> GetAll()
        {
            return await _db.Inventories.ToListAsync();
        }
        public async Task<Inventories> GetInventories(int Id_Inventory)
        {
            return await _db.Inventories.FirstOrDefaultAsync(i => i.Id_Inventory == Id_Inventory);
        }
        public async Task<Inventories> UpdateInventories(Inventories inventories)
        {
            Inventories ConsultUpdate = await _db.Inventories.FindAsync(inventories.Id_Inventory);
            if (ConsultUpdate != null)
            {
                ConsultUpdate.Number_Inventorie = inventories.Number_Inventorie;
                ConsultUpdate.Id_Person = inventories.Id_Person;
                ConsultUpdate.Id_Tool = inventories.Id_Tool;
                ConsultUpdate.Id_Epp = inventories.Id_Epp;
                await _db.SaveChangesAsync();
            }
            return ConsultUpdate;
            throw new NotImplementedException();
        }
        public async Task<Inventories> DeleteInventories(Inventories inventories)
        {
            _db.Inventories.Attach(inventories);
            _db.Entry(inventories).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return inventories;
        }
    }
}
