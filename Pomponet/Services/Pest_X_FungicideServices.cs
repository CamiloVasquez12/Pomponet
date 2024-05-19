using Pomponet.Context;
using Pomponet.Model;
using Pomponet.Repositories;

namespace Pomponet.Services
{
    public interface IPest_X_FungicideService
    {
        Task<List<Pest_X_Fungicide>> GetAll();
        Task<Pest_X_Fungicide> GetPest_X_Fungicide(int Id_Pest_X_Fungicide);
        Task<Pest_X_Fungicide> CreatePest_X_Fungicide(int Id_Pest, int Id_Fungicide);
        Task<Pest_X_Fungicide> UpdatePest_X_Fungicide(int Id_Pest_X_Fungicide, int? Id_Pest = null, int? Id_Fungicide = null);
        Task<Pest_X_Fungicide> DeletePest_X_Fungicide(int Id_Pest_X_Fungicide);
    }
    public class Pest_X_FungicideService : IPest_X_FungicideService
    {
        private readonly IPest_X_FungicideRepository _pest_x_fungicideRepository;
        public Pest_X_FungicideService(IPest_X_FungicideRepository pest_x_fungicideRepository)
        {
            _pest_x_fungicideRepository = pest_x_fungicideRepository;
        }
        public async Task<Pest_X_Fungicide> CreatePest_X_Fungicide(int id_pest, int id_fungicide)
        {
            return await _pest_x_fungicideRepository.CreatePest_X_Fungicide(id_pest, id_fungicide);
        }
        public async Task<List<Pest_X_Fungicide>> GetAll()
        {
            return await _pest_x_fungicideRepository.GetAll();
        }
        public async Task<Pest_X_Fungicide> GetPest_X_Fungicide(int Id_Pest_X_Fungicide)
        {
            return await _pest_x_fungicideRepository.GetPest_X_Fungicide(Id_Pest_X_Fungicide);
        }
        public async Task<Pest_X_Fungicide> UpdatePest_X_Fungicide(int Id_Pest_X_Fungicide, int? Id_Pest = null, int? Id_Fungicide = null)
        {
            Pest_X_Fungicide newPest_X_Fungicide = await _pest_x_fungicideRepository.GetPest_X_Fungicide(Id_Pest_X_Fungicide);
            if (newPest_X_Fungicide != null)
            {
                if (Id_Pest != null)
                {
                    newPest_X_Fungicide.Id_Pest = (int)Id_Pest;
                }
                if (Id_Fungicide != null)
                {
                    newPest_X_Fungicide.Id_Fungicide = (int)Id_Fungicide;
                }
                return await _pest_x_fungicideRepository.UpdatePest_X_Fungicide(newPest_X_Fungicide);
            }
            throw new NotImplementedException("Registro no encontrado");
        }
        public async Task<Pest_X_Fungicide> DeletePest_X_Fungicide(int Id_Pest_X_Fungicide)
        {
            Pest_X_Fungicide pest_X_Fungicide = await _pest_x_fungicideRepository.GetPest_X_Fungicide(Id_Pest_X_Fungicide);
            if (pest_X_Fungicide == null)
            {
                throw new Exception($"This Pest_X_Fungicide with the id {Id_Pest_X_Fungicide} don't exist. ");
            }
            pest_X_Fungicide.Deleted = true;

            return await _pest_x_fungicideRepository.DeletePest_X_Fungicide(pest_X_Fungicide);
        }
    }
}
