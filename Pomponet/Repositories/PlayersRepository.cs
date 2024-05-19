using Pomponet.Context;
using Pomponet.Model;
using Microsoft.EntityFrameworkCore;

namespace Pomponet.Repositories
{
    public interface IPlayersRepository
    {
        Task<List<Players>> GetAll();
        Task<Players> GetPlayers(int Id_Player);
        Task<Players> CreatePlayers(int Score, int Id_Person);
        Task<Players> UpdatePlayers(Players players);
        Task<Players> DeletePlayers(Players players);
    }
    public class PlayersRepository : IPlayersRepository
    {
        private readonly CropsDbContext _db;
        public PlayersRepository(CropsDbContext db)
        {
            _db = db;
        }
        public async Task<Players> CreatePlayers(int score, int id_person)
        {
            Players newPlayers = new Players
            {
                Score = score,
                Id_Person = id_person,
            };
            await _db.Players.AddAsync(newPlayers);
            _db.SaveChanges();
            return newPlayers;
        }
        public async Task<List<Players>> GetAll()
        {
            return await _db.Players.ToListAsync();
        }
        public async Task<Players> GetPlayers(int Id_Player)
        {
            return await _db.Players.FirstOrDefaultAsync(p => p.Id_Player == Id_Player);
        }
        public async Task<Players> UpdatePlayers(Players players)
        {
            Players ConsultUpdate = await _db.Players.FindAsync(players.Id_Player);
            if (ConsultUpdate != null)
            {
                ConsultUpdate.Score = players.Score;
                ConsultUpdate.Id_Person = players.Id_Person;
                await _db.SaveChangesAsync();
            }
            return ConsultUpdate;
            throw new NotImplementedException();
        }
        public async Task<Players> DeletePlayers(Players players)
        {
            _db.Players.Attach(players);
            _db.Entry(players).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return players;
        }
    }
}
