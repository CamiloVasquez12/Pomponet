using System.ComponentModel.DataAnnotations;
namespace PomponetWebsite.Models
{
    public class Pest_X_Fungicide
    {
        [Key]
        public int Id_Pest_X_Fungicide { get; set; }
        public required int Id_Pest { get; set; }
        public Pests? Pests { get; set; }
        public required int Id_Fungicide { get; set; }
        public Fungicides? Fungicides {  get; set; }
        public bool Deleted { get; set; }
    }
}
