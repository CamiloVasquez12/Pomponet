using Pomponet.Context;
using Pomponet.Model;
using Pomponet.Repositories;

namespace Pomponet.Services
{
    public interface IAchievementsService
    {
        Task<List<Achievements>> GetAll();
        Task<Achievements> GetAchievements(int Id_Achievement);
        Task<Achievements> CreateAchievements(string Achievement);
        Task<Achievements> UpdateAchievements(int Id_Achievement, string? Achievement = null);
        Task<Achievements> DeleteAchievements(int Id_Achievement);
    }
    public class AchievementsService : IAchievementsService
    {
        private readonly IAchievementsRepository _achievementsRepository;
        public AchievementsService(IAchievementsRepository achievementsRepository)
        {
            _achievementsRepository = achievementsRepository;
        }
        public async Task<Achievements> CreateAchievements(string achievement)
        {
            return await _achievementsRepository.CreateAchievements(achievement);
        }
        public async Task<List<Achievements>> GetAll()
        {
            return await _achievementsRepository.GetAll();
        }
        public async Task<Achievements> GetAchievements(int Id_Achievement)
        {
            return await _achievementsRepository.GetAchievements(Id_Achievement);
        }
        public async Task<Achievements> UpdateAchievements(int Id_Achievement, string? Achievement = null)
        {
            Achievements newAchievement = await _achievementsRepository.GetAchievements(Id_Achievement);
            if (newAchievement != null)
            {
                if (Achievement != null)
                {
                    newAchievement.Achievement = Achievement;
                }
                return await _achievementsRepository.UpdateAchievements(newAchievement);
            }
            throw new NotImplementedException("Registro no encontrado");
        }
        public async Task<Achievements> DeleteAchievements(int Id_Achievement)
        {
            Achievements achievementsToDelete = await _achievementsRepository.GetAchievements(Id_Achievement);
            if(achievementsToDelete == null) 
            {
                throw new Exception($"This Achievement with the Id {Id_Achievement} don't exist. ");
            }
            achievementsToDelete.Deleted = true;
           
            return await _achievementsRepository.DeleteAchievements(achievementsToDelete);
        }
    }
}
