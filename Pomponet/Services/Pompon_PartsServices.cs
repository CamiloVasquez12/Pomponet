using Pomponet.Context;
using Pomponet.Model;
using Pomponet.Repositories;

namespace Pomponet.Services
{
    public interface IPompon_PartsService
    {
        Task<List<Pompon_Parts>> GetAll();
        Task<Pompon_Parts> GetPompon_Parts(int Id_Pompon_Part);
        Task<Pompon_Parts> CreatePompon_Parts(string Part);
        Task<Pompon_Parts> UpdatePompon_Parts(int Id_Pompon_Part, string? Part = null);
        Task<Pompon_Parts> DeletePompon_Parts(int Id_Pompon_Part);
    }
    public class Pompon_PartsService : IPompon_PartsService
    {
        private readonly IPompo_PartsnRepository _pompon_partsRepository;
        public Pompon_PartsService(IPompo_PartsnRepository pompon_partsRepository)
        {
            _pompon_partsRepository = pompon_partsRepository;
        }
        public async Task<Pompon_Parts> CreatePompon_Parts(string part)
        {
            return await _pompon_partsRepository.CreatePompon_Parts(part);
        }
        public async Task<List<Pompon_Parts>> GetAll()
        {
            return await _pompon_partsRepository.GetAll();
        }
        public async Task<Pompon_Parts> GetPompon_Parts(int Id_Pompon_Part)
        {
            return await _pompon_partsRepository.GetPompon_Parts(Id_Pompon_Part);
        }
        public async Task<Pompon_Parts> UpdatePompon_Parts(int Id_Pompon_Part, string? Part = null)
        {
            Pompon_Parts newPompon_Part = await _pompon_partsRepository.GetPompon_Parts(Id_Pompon_Part);
            if (newPompon_Part != null)
            {
                if (Part != null)
                {
                    newPompon_Part.Part = Part;
                }
                return await _pompon_partsRepository.UpdatePompon_Parts(newPompon_Part);
            }
            throw new NotImplementedException("Registro no encontrado");
        }
        public async Task<Pompon_Parts> DeletePompon_Parts(int Id_Pompon_Part)
        {
            Pompon_Parts pompon_Parts = await _pompon_partsRepository.GetPompon_Parts(Id_Pompon_Part);
            if (pompon_Parts == null)
            {
                throw new Exception($"This Pompon_Part with the id {Id_Pompon_Part} don't exist. ");
            }
            pompon_Parts.Deleted = true;

            return await _pompon_partsRepository.DeletePompon_Parts(pompon_Parts);
        }
    }
}
