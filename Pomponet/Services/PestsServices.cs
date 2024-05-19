using Pomponet.Context;
using Pomponet.Model;
using Pomponet.Repositories;

namespace Pomponet.Services
{
    public interface IPestsService
    {
        Task<List<Pests>> GetAll();
        Task<Pests> GetPests(int Id_Pest);
        Task<Pests> CreatePests(string Pest);
        Task<Pests> UpdatePests(int Id_Pest, string? Pest = null);
        Task<Pests> DeletePests(int Id_Pest);
    }
    public class PestsService : IPestsService
    {
        private readonly IPestsRepository _pestsRepository;
        public PestsService(IPestsRepository pestsRepository)
        {
            _pestsRepository = pestsRepository;
        }
        public async Task<Pests> CreatePests(string pests)
        {
            return await _pestsRepository.CreatePests(pests);
        }
        public async Task<List<Pests>> GetAll()
        {
            return await _pestsRepository.GetAll();
        }
        public async Task<Pests> GetPests(int Id_Pest)
        {
            return await _pestsRepository.GetPests(Id_Pest);
        }
        public async Task<Pests> UpdatePests(int Id_Pest, string? Pest = null)
        {
            Pests newPest = await _pestsRepository.GetPests(Id_Pest);
            if (newPest != null)
            {
                if (Pest != null)
                {
                    newPest.Pest = Pest;
                }
                return await _pestsRepository.UpdatePests(newPest);
            }
            throw new NotImplementedException("Registro no encontrado");
        }
        public async Task<Pests> DeletePests(int Id_Pest)
        {
            Pests pests = await _pestsRepository.GetPests(Id_Pest);
            if (pests == null)
            {
                throw new Exception($"This Pest with the id {Id_Pest} don't exist. ");
            }
            pests.Deleted = true;

            return await _pestsRepository.DeletePests(pests);
        }
    }
}
