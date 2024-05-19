using Pomponet.Context;
using Pomponet.Model;
using Pomponet.Repositories;

namespace Pomponet.Services
{
    public interface IInventoriesService
    {
        Task<List<Inventories>> GetAll();
        Task<Inventories> GetInventories(int Id_Inventory);
        Task<Inventories> CreateInventories(int Number_Inventorie, int Id_Person, int Id_Tool, int Id_Epp);
        Task<Inventories> UpdateInventories(int Id_Inventory, int? Number_Inventorie = null, int? Id_Person = null, int? Id_Tool = null, int? Id_Epp = null);
        Task<Inventories> DeleteInventories(int Id_Inventory);
    }
    public class InventoriesService : IInventoriesService
    {
        private readonly IInventoriesRepository _inventoriesRepository;
        public InventoriesService(IInventoriesRepository inventoriesRepository)
        {
            _inventoriesRepository = inventoriesRepository;
        }
        public async Task<Inventories> CreateInventories(int number_inventorie, int id_person, int id_tool, int id_epp)
        {
            return await _inventoriesRepository.CreateInventories(number_inventorie, id_person, id_tool, id_epp);
        }
        public async Task<List<Inventories>> GetAll()
        {
            return await _inventoriesRepository.GetAll();
        }
        public async Task<Inventories> GetInventories(int Id_Inventory)
        {
            return await _inventoriesRepository.GetInventories(Id_Inventory);
        }
        public async Task<Inventories> UpdateInventories(int Id_Inventory, int? Number_Inventorie = null, int? Id_Person = null, int? Id_Tool = null, int? Id_Epp = null)
        {
            Inventories newInventorie = await _inventoriesRepository.GetInventories(Id_Inventory);
            if (newInventorie != null)
            {
                if (Number_Inventorie != null)
                {
                    newInventorie.Number_Inventorie = (int)Number_Inventorie;
                }
                if (Id_Person != null)
                {
                    newInventorie.Id_Person = (int)Id_Person;
                }
                if (Id_Tool != null)
                {
                    newInventorie.Id_Tool = (int)Id_Tool;
                }
                if (Id_Epp != null)
                {
                    newInventorie.Id_Epp = (int)Id_Epp;
                }
                return await _inventoriesRepository.UpdateInventories(newInventorie);
            }
            throw new NotImplementedException("Registro no encontrado");
        }
        public async Task<Inventories> DeleteInventories(int Id_Inventory)
        {
            Inventories inventories = await _inventoriesRepository.GetInventories(Id_Inventory);
            if (inventories == null)
            {
                throw new Exception($"This Inventory with the id {Id_Inventory} don't exist. ");
            }
            inventories.Deleted = true;

            return await _inventoriesRepository.DeleteInventories(inventories);
        }
    }
}
