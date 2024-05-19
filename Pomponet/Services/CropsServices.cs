using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Pomponet.Context;
using Pomponet.Model;
using Pomponet.Repositories;

namespace Pomponet.Services
{
    public interface ICropsService
    {
        Task<List<Crops>> GetAll();
        Task<Crops> GetCrops(int Id_Crop);
        Task<Crops> CreateCrops(int Crop_Number, int Id_Player);
        Task<Crops> UpdateCrops(int Id_Crop, int? Crop_Number = null, int? Id_Player = null);
        Task<Crops> DeleteCrops(int Id_Crop);
    }
    public class CropsService : ICropsService
    {
        private readonly ICropsRepository _cropsRepository;
        public CropsService(ICropsRepository cropsRepository)
        {
            _cropsRepository = cropsRepository;
        }
        public async Task<Crops> CreateCrops(int crop_number, int id_player)
        {
            return await _cropsRepository.CreateCrops(crop_number, id_player);
        }
        public async Task<List<Crops>> GetAll()
        {
            return await _cropsRepository.GetAll();
        }
        public async Task<Crops> GetCrops(int Id_Crop)
        {
            return await _cropsRepository.GetCrops(Id_Crop);
        }
        public async Task<Crops> UpdateCrops(int Id_Crop, int? Crop_Number = null, int? Id_Player = null)
        {
            Crops newCrop = await _cropsRepository.GetCrops(Id_Crop);
            if (newCrop != null)
            {
                if (Crop_Number != null)
                {
                    newCrop.Crop_Number = (int)Crop_Number;
                }
                if (Id_Player != null)
                {
                    newCrop.Id_Player = (int)Id_Player;
                }
                return await _cropsRepository.UpdateCrops(newCrop);
            }
            throw new NotImplementedException("Registro no encontrado");
        }
        public async Task<Crops> DeleteCrops(int Id_Crop)
        {
            Crops crops = await _cropsRepository.GetCrops(Id_Crop);
            if (crops == null)
            {
                throw new Exception($"This crop with the id {Id_Crop} don't exist. ");
            }
            crops.Deleted = true;

            return await _cropsRepository.DeleteCrops(crops);
        }
    }
}