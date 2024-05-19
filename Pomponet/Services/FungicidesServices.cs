using Pomponet.Context;
using Pomponet.Model;
using Pomponet.Repositories;

namespace Pomponet.Services
{
    public interface IFungicidesService
    {
        Task<List<Fungicides>> GetAll();
        Task<Fungicides> GetFungicides(int Id_Fungicide);
        Task<Fungicides> CreateFungicides(string Name_Fungicide, int Quantity, string Descripcion, int Id_crop);
        Task<Fungicides> UpdateFungicides(int Id_Fungicide, string? Name_Fungicide = null, int? Quantity = null, string? Descripcion = null, int? Id_crop = null);
        Task<Fungicides> DeleteFungicides(int Id_Fungicides);
    }
    public class FungicidesService : IFungicidesService
    {
        private readonly IFungicidesRepository _fungicidesRepository;
        public FungicidesService(IFungicidesRepository fungicidesRepository)
        {
            _fungicidesRepository = fungicidesRepository;
        }
        public async Task<Fungicides> CreateFungicides(string name_fungicide, int quantity, string descripcion, int id_crop)
        {
            return await _fungicidesRepository.CreateFungicides(name_fungicide, quantity, descripcion, id_crop);
        }
        public async Task<List<Fungicides>> GetAll()
        {
            return await _fungicidesRepository.GetAll();
        }
        public async Task<Fungicides> GetFungicides(int Id_Fungicide)
        {
            return await _fungicidesRepository.GetFungicides(Id_Fungicide);
        }
        public async Task<Fungicides> UpdateFungicides(int Id_Fungicide, string? Name_Fungicide = null, int? Quantity = null, string? Descripcion = null, int? Id_crop = null)
        {
            Fungicides newFungicide = await _fungicidesRepository.GetFungicides(Id_Fungicide);
            if (newFungicide != null)
            {
                if (Name_Fungicide != null)
                {
                    newFungicide.Name_Fungicide = Name_Fungicide;
                }
                if (Quantity != null)
                {
                    newFungicide.Quantity = (int)Quantity;
                }
                if (Descripcion != null)
                {
                    newFungicide.Description = Descripcion;
                }
                if (Id_crop != null)
                {
                    newFungicide.Id_crop = (int)Id_crop;
                }
                return await _fungicidesRepository.UpdateFungicides(newFungicide);
            }
            throw new NotImplementedException("Registro no encontrado");
        }
        public async Task<Fungicides> DeleteFungicides(int Id_Fungicides)
        {
            Fungicides fungicides = await _fungicidesRepository.GetFungicides(Id_Fungicides);
            if (fungicides == null)
            {
                throw new Exception($"This Fungicide with the id {Id_Fungicides} don't exist. ");
            }
            fungicides.Deleted = true;

            return await _fungicidesRepository.DeleteFungicides(fungicides);
        }
    }
}
