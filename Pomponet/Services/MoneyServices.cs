using Pomponet.Context;
using Pomponet.Model;
using Pomponet.Repositories;

namespace Pomponet.Services
{
    public interface IMoneyService
    {
        Task<List<Money>> GetAll();
        Task<Money> GetMoney(int Id_Money);
        Task<Money> CreateMoney(int Quantity, int Id_Player);
        Task<Money> UpdateMoney(int Id_Money, int? Quantity = null, int? Id_Player = null);
        Task<Money> DeleteMoney(int Id_Money);
    }
    public class MoneyService : IMoneyService
    {
        private readonly IMoneyRepository _moneyRepository;
        public MoneyService(IMoneyRepository moneyRepository)
        {
            _moneyRepository = moneyRepository;
        }
        public async Task<Money> CreateMoney(int quantity, int id_player)
        {
            return await _moneyRepository.CreateMoney(quantity, id_player);
        }
        public async Task<List<Money>> GetAll()
        {
            return await _moneyRepository.GetAll();
        }
        public async Task<Money> GetMoney(int Id_Money)
        {
            return await _moneyRepository.GetMoney(Id_Money);
        }
        public async Task<Money> UpdateMoney(int Id_Money, int? Quantity = null, int? Id_Player = null)
        {
            Money newMoney = await _moneyRepository.GetMoney(Id_Money);
            if (newMoney != null)
            {
                if (Quantity != null)
                {
                    newMoney.Quantity = (int)Quantity;
                }
                if (Id_Player != null)
                {
                    newMoney.Id_Player = (int)Id_Player;
                }
                return await _moneyRepository.UpdateMoney(newMoney);
            }
            throw new NotImplementedException("Registro no encontrado");
        }
        public async Task<Money> DeleteMoney(int Id_Money)
        {
            Money money = await _moneyRepository.GetMoney(Id_Money);
            if (money == null)
            {
                throw new Exception($"This Money with the id {Id_Money} don't exist. ");
            }
            money.Deleted = true;

            return await _moneyRepository.DeleteMoney(money);
        }
    }
}
