using Pomponet.Context;
using Pomponet.Model;
using Pomponet.Repositories;

namespace Pomponet.Services
{
    public interface IAplicationToolsService
    {
        Task<List<AplicationTools>> GetAll();
        Task<AplicationTools> GetAplicationTools(int Id_AplicationTool);
        Task<AplicationTools> CreateAplicationTools(string Tool, string Quantity, string Description, int Price);
        Task<AplicationTools> UpdateAplicationTools(int Id_AplicationTool, string? Tool = null, string? Quantity = null, string? Description = null, int? Price = null);
        Task<AplicationTools> DeleteAplicationTools(int Id_AplicationTool);
    }
    public class AplicationToolsService : IAplicationToolsService
    {
        private readonly IAplicationToolsRepository _aplicationtoolsRepository;
        public AplicationToolsService(IAplicationToolsRepository aplicationtoolsRepository)
        {
            _aplicationtoolsRepository = aplicationtoolsRepository;
        }
        public async Task<AplicationTools> CreateAplicationTools(string tool, string quantity, string description, int price)
        {
            return await _aplicationtoolsRepository.CreateAplicationTools(tool, quantity, description, price);
        }
        public async Task<List<AplicationTools>> GetAll()
        {
            return await _aplicationtoolsRepository.GetAll();
        }
        public async Task<AplicationTools> GetAplicationTools(int Id_AplicationTool)
        {
            return await _aplicationtoolsRepository.GetAplicationTools(Id_AplicationTool);
        }
        public async Task<AplicationTools> UpdateAplicationTools(int Id_AplicationTool, string? Tool = null, string? Quantity = null, string? Description = null, int? Price = null)
        {
            AplicationTools newAplicationTool = await _aplicationtoolsRepository.GetAplicationTools(Id_AplicationTool);
            if (newAplicationTool != null)
            {
                if (Tool != null)
                {
                    newAplicationTool.Tool = Tool;
                }
                if (Quantity != null)
                {
                    newAplicationTool.Quantity = Quantity;
                }
                if (Description != null)
                {
                    newAplicationTool.Description = Description;
                }
                if (Price != null)
                {
                    newAplicationTool.Price = (int)Price;
                }
                return await _aplicationtoolsRepository.UpdateAplicationTools(newAplicationTool);
            }
            throw new NotImplementedException("Registro no encontrado");
        }
        public async Task<AplicationTools> DeleteAplicationTools(int Id_AplicationTool)
        {
            AplicationTools aplicationtools = await _aplicationtoolsRepository.GetAplicationTools(Id_AplicationTool);
            if(aplicationtools == null)
            {
                throw new Exception($"This AplicationTool with the id {Id_AplicationTool} don't exist. ");
            }
            aplicationtools.Deleted = true;

            return await _aplicationtoolsRepository.DeleteAplicationTools(aplicationtools);
        }
    }
}
