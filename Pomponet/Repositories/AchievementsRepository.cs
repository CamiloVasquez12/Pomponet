using Pomponet.Context;
using Pomponet.Model;
using Microsoft.EntityFrameworkCore;


namespace Pomponet.Repositories
{
    public interface IAchievementsRepository
    {
        Task<List<Achievements>> GetAll();
        Task<Achievements> GetAchievements(int Id_Achievement);
        Task<Achievements> CreateAchievements(string Achievement);
        Task<Achievements> UpdateAchievements(Achievements achievements);
        Task<Achievements> DeleteAchievements(Achievements achievements);
    }
    public class AchievementsRepository : IAchievementsRepository
    {
        private readonly CropsDbContext _db;
        public AchievementsRepository(CropsDbContext db)
        {
            _db = db;
        }
        public async Task<Achievements> CreateAchievements(string achievement)
        {
            Achievements newAchievements = new Achievements
            {
                Achievement = achievement,
            };
            await _db.Achievements.AddAsync(newAchievements);
            _db.SaveChanges();
            return newAchievements;
        }
        public async Task<List<Achievements>> GetAll()
        {
            return await _db.Achievements.ToListAsync();
        }
        public async Task<Achievements> GetAchievements(int Id_Achievement)
        {
            return await _db.Achievements.FirstOrDefaultAsync(a => a.Id_Achievement == Id_Achievement);
        }
        public async Task<Achievements> UpdateAchievements(Achievements achievements)
        {
            Achievements ConsultUpdate = await _db.Achievements.FindAsync(achievements.Id_Achievement);
            if (ConsultUpdate != null)
            {
                ConsultUpdate.Achievement = achievements.Achievement;
                await _db.SaveChangesAsync();
            }
            return ConsultUpdate;
            throw new NotImplementedException();
        }
        public async Task<Achievements> DeleteAchievements(Achievements achievements)
        {
            _db.Achievements.Attach(achievements);
            _db.Entry(achievements).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return achievements;
        }
    }
}