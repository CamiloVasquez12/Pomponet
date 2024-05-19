using Pomponet.Context;
using Pomponet.Model;
using Microsoft.EntityFrameworkCore;

namespace Pomponet.Repositories
{
    public interface IPlayer_AchievementsRepository
    {
        Task<List<Player_Achievements>> GetAll();
        Task<Player_Achievements> GetPlayer_Achievements(int Id_Player_Achievement);
        Task<Player_Achievements> CreatePlayer_Achievements(int Id_Achievement, int Logros_Totales, int Id_Player);
        Task<Player_Achievements> UpdatePlayer_Achievements(Player_Achievements player_achievements);
        Task<Player_Achievements> DeletePlayer_Achievements(Player_Achievements player_achievements);
    }
    public class Player_AchievementsRepository : IPlayer_AchievementsRepository
    {
        private readonly CropsDbContext _db;
        public Player_AchievementsRepository(CropsDbContext db)
        {
            _db = db;
        }
        public async Task<Player_Achievements> CreatePlayer_Achievements(int id_achievement, int logros_totales, int id_player)
        {
            Player_Achievements newPlayer_Achievements = new Player_Achievements
            {
                Id_Achievement = id_achievement,
                Logros_Totales = logros_totales,
                Id_Player = id_player,
            };
            await _db.Player_Achievements.AddAsync(newPlayer_Achievements);
            _db.SaveChanges();
            return newPlayer_Achievements;
        }
        public async Task<List<Player_Achievements>> GetAll()
        {
            return await _db.Player_Achievements.ToListAsync();
        }
        public async Task<Player_Achievements> GetPlayer_Achievements(int Id_Player_Achievement)
        {
            return await _db.Player_Achievements.FirstOrDefaultAsync(p => p.Id_Player_Achievement == Id_Player_Achievement);
        }
        public async Task<Player_Achievements> UpdatePlayer_Achievements(Player_Achievements player_achievements)
        {
            Player_Achievements ConsultUpdate = await _db.Player_Achievements.FindAsync(player_achievements.Id_Player_Achievement);
            if (ConsultUpdate != null)
            {
                ConsultUpdate.Id_Achievement = player_achievements.Id_Achievement;
                ConsultUpdate.Logros_Totales = player_achievements.Logros_Totales;
                ConsultUpdate.Id_Player = player_achievements.Id_Player;
                await _db.SaveChangesAsync();
            }
            return ConsultUpdate;
            throw new NotImplementedException();
        }
        public async Task<Player_Achievements> DeletePlayer_Achievements(Player_Achievements player_achievements)
        {
            _db.Player_Achievements.Attach(player_achievements);
            _db.Entry(player_achievements).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return player_achievements;
        }
    }
}
