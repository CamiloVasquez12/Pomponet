using Pomponet.Context;
using Pomponet.Model;
using Microsoft.EntityFrameworkCore;

namespace Pomponet.Repositories
{
    public interface IAplicationToolsRepository
    {
        Task<List<AplicationTools>> GetAll();
        Task<AplicationTools> GetAplicationTools(int Id_AplicationTool);
        Task<AplicationTools> CreateAplicationTools(string Tool, string Quantity, string Description, int Price);
        Task<AplicationTools> UpdateAplicationTools(AplicationTools aplicationtools);
        Task<AplicationTools> DeleteAplicationTools(AplicationTools aplicationtools);
    }
    public class AplicationToolsRepository : IAplicationToolsRepository
    {
        private readonly CropsDbContext _db;
        public AplicationToolsRepository(CropsDbContext db)
        {
            _db = db;
        }
        public async Task<AplicationTools> CreateAplicationTools(string tool, string quantity, string description, int price)
        {
            AplicationTools newAplicationTools = new AplicationTools
            {
                Tool = tool,
                Quantity = quantity,
                Description = description,
                Price = price,
            };
            await _db.AplicationTools.AddAsync(newAplicationTools);
            _db.SaveChanges();
            return newAplicationTools;
        }
        public async Task<List<AplicationTools>> GetAll()
        {
            return await _db.AplicationTools.ToListAsync();
        }
        public async Task<AplicationTools> GetAplicationTools(int Id_AplicationTool)
        {
            return await _db.AplicationTools.FirstOrDefaultAsync(a => a.Id_AplicationTool == Id_AplicationTool);
        }
        public async Task<AplicationTools> UpdateAplicationTools(AplicationTools aplicationtools)
        {
            AplicationTools ConsultUpdate = await _db.AplicationTools.FindAsync(aplicationtools.Id_AplicationTool);
            if (ConsultUpdate != null)
            {
                ConsultUpdate.Tool = aplicationtools.Tool;
                ConsultUpdate.Quantity = aplicationtools.Quantity;
                ConsultUpdate.Description = aplicationtools.Description;
                ConsultUpdate.Price = aplicationtools.Price;
                await _db.SaveChangesAsync();
            }
            return ConsultUpdate;
            throw new NotImplementedException();
        }
        public async Task<AplicationTools> DeleteAplicationTools(AplicationTools aplicationtools)
        {
            _db.AplicationTools.Attach(aplicationtools);
            _db.Entry(aplicationtools).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return aplicationtools;
        }
    }
}
