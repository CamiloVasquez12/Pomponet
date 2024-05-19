using Pomponet.Context;
using Pomponet.Model;
using Pomponet.Repositories;

namespace Pomponet.Services
{
    public interface IPlayersService
    {
        Task<List<Players>> GetAll();
        Task<Players> GetPlayers(int Id_Player);
        Task<Players> CreatePlayers(int Score, int Id_Person);
        Task<Players> UpdatePlayers(int Id_Player, int? Score = null, int? Id_Person = null);
        Task<Players> DeletePlayers(int Id_Player);
    }
    public class PlayersService : IPlayersService
    {
        private readonly IPlayersRepository _playersRepository;
        public PlayersService(IPlayersRepository playersRepository)
        {
            _playersRepository = playersRepository;
        }
        public async Task<Players> CreatePlayers(int score, int id_person)
        {
            return await _playersRepository.CreatePlayers(score, id_person);
        }
        public async Task<List<Players>> GetAll()
        {
            return await _playersRepository.GetAll();
        }
        public async Task<Players> GetPlayers(int Id_Player)
        {
            return await _playersRepository.GetPlayers(Id_Player);
        }
        public async Task<Players> UpdatePlayers(int Id_Player, int? Score = null, int? Id_Person = null)
        {
            Players newPlayer = await _playersRepository.GetPlayers(Id_Player);
            if (newPlayer != null)
            {
                if (Score != null)
                {
                    newPlayer.Score = (int)Score;
                }
                if (Id_Person != null)
                {
                    newPlayer.Id_Person = (int)Id_Person;
                }
                return await _playersRepository.UpdatePlayers(newPlayer);
            }
            throw new NotImplementedException("Registro no encontrado");
        }
        public async Task<Players> DeletePlayers(int Id_Player)
        {
            Players players = await _playersRepository.GetPlayers(Id_Player);
            if (players == null)
            {
                throw new Exception($"This Player with the id {Id_Player} don't exist. ");
            }
            players.Deleted = true;

            return await _playersRepository.DeletePlayers(players);
        }
    }
}
