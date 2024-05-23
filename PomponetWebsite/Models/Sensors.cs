using System.ComponentModel.DataAnnotations;
namespace PomponetWebsite.Models
{
    public class Sensors
    {
        [Key]
        public int Id_Sensor { get; set; }
        public required string Sensor { get; set; }
        public required int Price { get; set; }
        public required string Description { get; set; }
        public required int Id_crop {  get; set; }
        public Crops? Crops { get; set; }
        public bool Deleted { get; set; }
    }
}
