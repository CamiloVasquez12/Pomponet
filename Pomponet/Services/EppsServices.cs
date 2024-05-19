using Pomponet.Context;
using Pomponet.Model;
using Pomponet.Repositories;

namespace Pomponet.Services
{
    public interface IEppsService
    {
        Task<List<Epps>> GetAll();
        Task<Epps> GetEpps(int Id_Epp);
        Task<Epps> CreateEpps(string Name_Epp);
        Task<Epps> UpdateEpps(int Id_Epp, string? Name_Epp = null);
        Task<Epps> DeleteEpps(int Id_Epp);
    }
    public class EppsService : IEppsService
    {
        private readonly IEppsRepository _eppsRepository;
        public EppsService(IEppsRepository eppsRepository)
        {
            _eppsRepository = eppsRepository;
        }
        public async Task<Epps> CreateEpps(string Name_Epp)
        {
            return await _eppsRepository.CreateEpps(Name_Epp);
        }
        public async Task<List<Epps>> GetAll()
        {
            return await _eppsRepository.GetAll();
        }
        public async Task<Epps> GetEpps(int Id_Epp)
        {
            return await _eppsRepository.GetEpps(Id_Epp);
        }
        public async Task<Epps> UpdateEpps(int Id_Epp, string? Name_Epp = null)
        {
            Epps newEpp = await _eppsRepository.GetEpps(Id_Epp);
            if (newEpp != null)
            {
                if (Name_Epp != null)
                {
                    newEpp.Name_Epp = Name_Epp;
                }
                return await _eppsRepository.UpdateEpps(newEpp);
            }
            throw new NotImplementedException("Registro no encontrado");
        }
        public async Task<Epps> DeleteEpps(int Id_Epp)
        {
            Epps epps = await _eppsRepository.GetEpps(Id_Epp);
            if (epps == null)
            {
                throw new Exception($"This Epp with the id {Id_Epp} don't exist. ");
            }
            epps.Deleted = true;

            return await _eppsRepository.DeleteEpps(epps);
        }
    }
}
