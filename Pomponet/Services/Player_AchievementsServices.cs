using Pomponet.Context;
using Pomponet.Model;
using Pomponet.Repositories;

namespace Pomponet.Services
{
    public interface IPlayer_AchievementsService
    {
        Task<List<Player_Achievements>> GetAll();
        Task<Player_Achievements> GetPlayer_Achievements(int Id_Player_Achievement);
        Task<Player_Achievements> CreatePlayer_Achievements(int Id_Achievement, int Logros_Totales, int Id_Player);
        Task<Player_Achievements> UpdatePlayer_Achievements(int Id_Player_Achievement, int? Id_Achievement = null, int? Logros_Totales = null, int? Id_Player = null);
        Task<Player_Achievements> DeletePlayer_Achievements(int Id_Player_Achievement);
    }
    public class Player_AchievementsService : IPlayer_AchievementsService
    {
        private readonly IPlayer_AchievementsRepository _player_achievementsRepository;
        public Player_AchievementsService(IPlayer_AchievementsRepository player_achievementsRepository)
        {
            _player_achievementsRepository = player_achievementsRepository;
        }
        public async Task<Player_Achievements> CreatePlayer_Achievements(int id_achievement, int logros_totales, int id_player)
        {
            return await _player_achievementsRepository.CreatePlayer_Achievements(id_achievement, logros_totales, id_player);
        }
        public async Task<List<Player_Achievements>> GetAll()
        {
            return await _player_achievementsRepository.GetAll();
        }
        public async Task<Player_Achievements> GetPlayer_Achievements(int Id_Player_Achievement)
        {
            return await _player_achievementsRepository.GetPlayer_Achievements(Id_Player_Achievement);
        }
        public async Task<Player_Achievements> UpdatePlayer_Achievements(int Id_Player_Achievement, int? Id_Achievement = null, int? Logros_Totales = null, int? Id_Player = null)
        {
            Player_Achievements newPlayer_Achievement = await _player_achievementsRepository.GetPlayer_Achievements(Id_Player_Achievement);
            if (newPlayer_Achievement != null)
            {
                if (Id_Achievement != null)
                {
                    newPlayer_Achievement.Id_Achievement = (int)Id_Achievement;
                }
                if (Logros_Totales != null)
                {
                    newPlayer_Achievement.Logros_Totales = (int)Logros_Totales;
                }
                if (Id_Player != null)
                {
                    newPlayer_Achievement.Id_Player = (int)Id_Player;
                }
                return await _player_achievementsRepository.UpdatePlayer_Achievements(newPlayer_Achievement);
            }
            throw new NotImplementedException("Registro no encontrado");
        }
        public async Task<Player_Achievements> DeletePlayer_Achievements(int Id_Player_Achievement)
        {
            Player_Achievements player_Achievements = await _player_achievementsRepository.GetPlayer_Achievements(Id_Player_Achievement);
            if (player_Achievements == null)
            {
                throw new Exception($"This Player_Achievement with the id {Id_Player_Achievement} don't exist. ");
            }
            player_Achievements.Deleted = true;

            return await _player_achievementsRepository.DeletePlayer_Achievements(player_Achievements);
        }
    }
}
