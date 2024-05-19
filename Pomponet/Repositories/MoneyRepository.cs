using Pomponet.Context;
using Pomponet.Model;
using Microsoft.EntityFrameworkCore;

namespace Pomponet.Repositories
{
    public interface IMoneyRepository
    {
        Task<List<Money>> GetAll();
        Task<Money> GetMoney(int Id_Money);
        Task<Money> CreateMoney(int Quantity, int Id_Player);
        Task<Money> UpdateMoney(Money money);
        Task<Money> DeleteMoney(Money money);
    }
    public class MoneyRepository : IMoneyRepository
    {
        private readonly CropsDbContext _db;
        public MoneyRepository(CropsDbContext db)
        {
            _db = db;
        }
        public async Task<Money> CreateMoney(int quantity, int id_player)
        {
            Money newMoney = new Money
            {
                Quantity = quantity,
                Id_Player = id_player,
            };
            await _db.Money.AddAsync(newMoney);
            _db.SaveChanges();
            return newMoney;
        }
        public async Task<List<Money>> GetAll()
        {
            return await _db.Money.ToListAsync();
        }
        public async Task<Money> GetMoney(int Id_Money)
        {
            return await _db.Money.FirstOrDefaultAsync(m => m.Id_Money == Id_Money);
        }
        public async Task<Money> UpdateMoney(Money money)
        {
            Money ConsultUpdate = await _db.Money.FindAsync(money.Id_Money);
            if (ConsultUpdate != null)
            {
                ConsultUpdate.Quantity = money.Quantity;
                ConsultUpdate.Id_Player = money.Id_Player;
                await _db.SaveChangesAsync();
            }
            return ConsultUpdate;
            throw new NotImplementedException();
        }
        public async Task<Money> DeleteMoney(Money money)
        {
            _db.Money.Attach(money);
            _db.Entry(money).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return money;
        }
    }
}
