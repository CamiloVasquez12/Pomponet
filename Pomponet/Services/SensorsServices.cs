using Pomponet.Context;
using Pomponet.Model;
using Pomponet.Repositories;

namespace Pomponet.Services
{
    public interface ISensorsService
    {
        Task<List<Sensors>> GetAll();
        Task<Sensors> GetSensors(int Id_Sensor);
        Task<Sensors> CreateSensors(string Sensor, int Price, string Description, int Id_Crop);
        Task<Sensors> UpdateSensors(int Id_Sensor, string? Sensor = null, int? Price = null, string? Description = null, int? Id_Crop = null);
        Task<Sensors> DeleteSensors(int Id_Sensor);
    }
    public class SensorsService : ISensorsService
    {
        private readonly ISensorsRepository _sensorsRepository;
        public SensorsService(ISensorsRepository sensorsRepository)
        {
            _sensorsRepository = sensorsRepository;
        }
        public async Task<Sensors> CreateSensors(string sensor, int price, string description, int id_crop)
        {
            return await _sensorsRepository.CreateSensors(sensor, price, description, id_crop);
        }
        public async Task<List<Sensors>> GetAll()
        {
            return await _sensorsRepository.GetAll();
        }
        public async Task<Sensors> GetSensors(int Id_Sensor)
        {
            return await _sensorsRepository.GetSensors(Id_Sensor);
        }
        public async Task<Sensors> UpdateSensors(int Id_Sensor, string? Sensor = null, int? Price = null, string? Description = null, int? Id_Crop = null)
        {
            Sensors newSensor = await _sensorsRepository.GetSensors(Id_Sensor);
            if (newSensor != null)
            {
                if (Sensor != null)
                {
                    newSensor.Sensor = Sensor;
                }
                if (Price != null)
                {
                    newSensor.Price = (int)Price;
                }
                if (Description != null)
                {
                    newSensor.Description = Description;
                }
                if (Id_Crop != null)
                {
                    newSensor.Id_crop = (int)Id_Crop;
                }
                return await _sensorsRepository.UpdateSensors(newSensor);
            }
            throw new NotImplementedException("Registro no encontrado");
        }
        public async Task<Sensors> DeleteSensors(int Id_Sensor)
        {
            Sensors sensors = await _sensorsRepository.GetSensors(Id_Sensor);
            if (sensors == null)
            {
                throw new Exception($"This Sensor with the id {Id_Sensor} don't exist. ");
            }
            sensors.Deleted = true;

            return await _sensorsRepository.DeleteSensors(sensors);
        }
    }
}
