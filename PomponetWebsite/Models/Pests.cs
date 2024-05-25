using System.ComponentModel.DataAnnotations;
namespace PomponetWebsite.Models
{
    public class Pests
    {
        [Key]
        public int Id_Pest { get; set; }
        public required string Pest { get; set; }
        public bool Deleted { get; set; }
        public ICollection<Pest_X_Fungicide> Pest_X_Fungicide { get; set; }
        public Pests()
        {
            Pest_X_Fungicide = new List<Pest_X_Fungicide>();
        }
    }
}
