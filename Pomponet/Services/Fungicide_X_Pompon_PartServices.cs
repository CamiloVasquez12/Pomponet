using Pomponet.Context;
using Pomponet.Model;
using Pomponet.Repositories;

namespace Pomponet.Services
{
    public interface IFungicide_X_Pompon_PartService
    {
        Task<List<Fungicide_X_Pompon_Part>> GetAll();
        Task<Fungicide_X_Pompon_Part> GetFungicide_X_Pompon_Part(int Id_Fungicide_X_Pompon_Part);
        Task<Fungicide_X_Pompon_Part> CreateFungicide_X_Pompon_Part(int Id_Pompon_Part, int Id_Fungicide);
        Task<Fungicide_X_Pompon_Part> UpdateFungicide_X_Pompon_Part(int Id_Fungicide_X_Pompon_Part, int? Id_Pompon_Part = null, int? Id_Fungicide = null);
        Task<Fungicide_X_Pompon_Part> DeleteFungicide_X_Pompon_Part(int Id_Fungicide_X_Pompon_Part);
    }
    public class Fungicide_X_Pompon_PartService : IFungicide_X_Pompon_PartService
    {
        private readonly IFungicide_X_Pompon_PartRepository _fungicide_x_pompon_partRepository;
        public Fungicide_X_Pompon_PartService(IFungicide_X_Pompon_PartRepository fungicide_x_pompon_partRepository)
        {
            _fungicide_x_pompon_partRepository = fungicide_x_pompon_partRepository;
        }
        public async Task<Fungicide_X_Pompon_Part> CreateFungicide_X_Pompon_Part(int id_pompon_part, int id_fungicide)
        {
            return await _fungicide_x_pompon_partRepository.CreateFungicide_X_Pompon_Part(id_pompon_part, id_fungicide);
        }
        public async Task<List<Fungicide_X_Pompon_Part>> GetAll()
        {
            return await _fungicide_x_pompon_partRepository.GetAll();
        }
        public async Task<Fungicide_X_Pompon_Part> GetFungicide_X_Pompon_Part(int Id_Fungicide_X_Pompon_Part)
        {
            return await _fungicide_x_pompon_partRepository.GetFungicide_X_Pompon_Part(Id_Fungicide_X_Pompon_Part);
        }
        public async Task<Fungicide_X_Pompon_Part> UpdateFungicide_X_Pompon_Part(int Id_Fungicide_X_Pompon_Part, int? Id_Pompon_Part = null, int? Id_Fungicide = null)
        {
            Fungicide_X_Pompon_Part newFungicide_X_Pompon_Part = await _fungicide_x_pompon_partRepository.GetFungicide_X_Pompon_Part(Id_Fungicide_X_Pompon_Part);
            if (newFungicide_X_Pompon_Part != null)
            {
                if (Id_Pompon_Part != null)
                {
                    newFungicide_X_Pompon_Part.Id_Pompon_Part = (int)Id_Pompon_Part;
                }
                if (Id_Fungicide != null)
                {
                    newFungicide_X_Pompon_Part.Id_Fungicide= (int)Id_Fungicide;
                }
                return await _fungicide_x_pompon_partRepository.UpdateFungicide_X_Pompon_Part(newFungicide_X_Pompon_Part);
            }
            throw new NotImplementedException("Registro no encontrado");
        }
        public async Task<Fungicide_X_Pompon_Part> DeleteFungicide_X_Pompon_Part(int Id_Fungicide_X_Pompon_Part)
        {
            Fungicide_X_Pompon_Part fungicide_x_pompon_part = await _fungicide_x_pompon_partRepository.GetFungicide_X_Pompon_Part(Id_Fungicide_X_Pompon_Part);
            if (fungicide_x_pompon_part == null)
            {
                throw new Exception($"This Fungicide_X_Pompon_Part with the id {Id_Fungicide_X_Pompon_Part} don't exist. ");
            }
            fungicide_x_pompon_part.Deleted = true;

            return await _fungicide_x_pompon_partRepository.DeleteFungicide_X_Pompon_Part(fungicide_x_pompon_part);
        }
    }
}
