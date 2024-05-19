using Pomponet.Context;
using Pomponet.Model;
using Pomponet.Repositories;

namespace Pomponet.Services
{
    public interface ITypes_FungicidesService
    {
        Task<List<Types_Fungicides>> GetAll();
        Task<Types_Fungicides> GetTypes_Fungicides(int Id_Type_Fungicide);
        Task<Types_Fungicides> CreateTypes_Fungicides(string Type_Fungicide, int Id_Funicides);
        Task<Types_Fungicides> UpdateTypes_Fungicides(int Id_Type_Fungicide, string? Type_Fungicide = null, int? Id_Funicides = null);
        Task<Types_Fungicides> DeleteTypes_Fungicides(int Id_Type_Fungicide);
    }
    public class Types_FungicidesService : ITypes_FungicidesService
    {
        private readonly ITypes_FungicidesRepository _types_fungicidesRepository;
        public Types_FungicidesService(ITypes_FungicidesRepository types_fungicidesRepository)
        {
            _types_fungicidesRepository = types_fungicidesRepository;
        }
        public async Task<Types_Fungicides> CreateTypes_Fungicides(string type_fungicide, int id_funicides)
        {
            return await _types_fungicidesRepository.CreateTypes_Fungicides(type_fungicide, id_funicides);
        }
        public async Task<List<Types_Fungicides>> GetAll()
        {
            return await _types_fungicidesRepository.GetAll();
        }
        public async Task<Types_Fungicides> GetTypes_Fungicides(int Id_Type_Fungicide)
        {
            return await _types_fungicidesRepository.GetTypes_Fungicides(Id_Type_Fungicide);
        }
        public async Task<Types_Fungicides> UpdateTypes_Fungicides(int Id_Type_Fungicide, string? Type_Fungicide = null, int? Id_Funicides = null)
        {
            Types_Fungicides newType_Fungicide = await _types_fungicidesRepository.GetTypes_Fungicides(Id_Type_Fungicide);
            if (newType_Fungicide != null)
            {
                if (Type_Fungicide != null)
                {
                    newType_Fungicide.Type_Fungicide = Type_Fungicide;
                }
                if (Id_Funicides != null)
                {
                    newType_Fungicide.Id_Funicides = (int)Id_Funicides;
                }
                return await _types_fungicidesRepository.UpdateTypes_Fungicides(newType_Fungicide);
            }
            throw new NotImplementedException("Registro no encontrado");
        }
        public async Task<Types_Fungicides> DeleteTypes_Fungicides(int Id_Type_Fungicide)
        {
            Types_Fungicides types_Fungicides = await _types_fungicidesRepository.GetTypes_Fungicides(Id_Type_Fungicide);
            if (types_Fungicides == null)
            {
                throw new Exception($"This Type_Fungicide with the id {Id_Type_Fungicide} don't exist. ");
            }
            types_Fungicides.Deleted = true;

            return await _types_fungicidesRepository.DeleteTypes_Fungicides(types_Fungicides);
        }
    }
}
