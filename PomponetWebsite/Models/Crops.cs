using System.ComponentModel.DataAnnotations;
namespace PomponetWebsite.Models
{
    public class Crops
    {
        [Key]
        public int Id_Crop { get; set; }
        public required int Crop_Number { get; set; }
        public required int Id_Player { get; set; }
        public Players? Players { get; set; }
        public bool Deleted { get; set; }
        public ICollection<Fungicides> Fungicides { get; set; }
        public Crops()
        {
            Fungicides = new List<Fungicides>();
        }
    }
}


