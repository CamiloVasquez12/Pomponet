using System.ComponentModel.DataAnnotations;
namespace PomponetWebsite.Models
{
    public class Fungicides
    {
        [Key]
        public int Id_Fungicide { get; set; }
        public required string Name_Fungicide { get; set; }
        public required int Quantity { get; set; }
        public required string Description { get; set; }
        public required int Id_crop { get; set; }
        public Crops? Crops { get; set; }
        public bool Deleted { get; set; }
        public ICollection<Fungicide_X_Pompon_Part> Fungicide_X_Pompon_Parts { get; set; }
        public Fungicides()
        {
            Fungicide_X_Pompon_Parts = new List<Fungicide_X_Pompon_Part>();
            Pest_X_Fungicide = new List<Pest_X_Fungicide>();
            Types_Fungicides = new List<Types_Fungicides>();
        }
        public ICollection<Pest_X_Fungicide> Pest_X_Fungicide { get; set; }
        public ICollection<Types_Fungicides> Types_Fungicides { get; set; }
    }
}
