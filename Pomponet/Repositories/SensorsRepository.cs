using Pomponet.Context;
using Pomponet.Model;
using Microsoft.EntityFrameworkCore;

namespace Pomponet.Repositories
{
    public interface ISensorsRepository
    {
        Task<List<Sensors>> GetAll();
        Task<Sensors> GetSensors(int Id_Sensor);
        Task<Sensors> CreateSensors(string Sensor, int Price, string Description, int Id_crop);
        Task<Sensors> UpdateSensors(Sensors sensors);
        Task<Sensors> DeleteSensors(Sensors sensors);
    }
    public class SensorsRepository : ISensorsRepository
    {
        private readonly CropsDbContext _db;
        public SensorsRepository(CropsDbContext db)
        {
            _db = db;
        }
        public async Task<Sensors> CreateSensors(string sensor, int price, string description, int id_crop)
        {
            Sensors newSensors = new Sensors
            {
                Sensor = sensor,
                Price = price,
                Description = description,
                Id_crop = id_crop,
            };
            await _db.Sensors.AddAsync(newSensors);
            _db.SaveChanges();
            return newSensors;
        }
        public async Task<List<Sensors>> GetAll()
        {
            return await _db.Sensors.ToListAsync();
        }
        public async Task<Sensors> GetSensors(int Id_Sensor)
        {
            return await _db.Sensors.FirstOrDefaultAsync(s => s.Id_Sensor == Id_Sensor);
        }
        public async Task<Sensors> UpdateSensors(Sensors sensors)
        {
            Sensors ConsultUpdate = await _db.Sensors.FindAsync(sensors.Id_Sensor);
            if (ConsultUpdate != null)
            {
                ConsultUpdate.Sensor = sensors.Sensor;
                ConsultUpdate.Price = sensors.Price;
                ConsultUpdate.Description = sensors.Description;
                ConsultUpdate.Id_crop = sensors.Id_crop;
                await _db.SaveChangesAsync();
            }
            return ConsultUpdate;
            throw new NotImplementedException();
        }
        public async Task<Sensors> DeleteSensors(Sensors sensors)
        {
            _db.Sensors.Attach(sensors);
            _db.Entry(sensors).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return sensors;
        }
    }
}
