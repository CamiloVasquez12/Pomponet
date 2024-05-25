using System.ComponentModel.DataAnnotations;
namespace PomponetWebsite.Models
{
    public class Pompon_Parts
    {
        [Key]
        public int Id_Pompon_Part { get; set; }
        public required string Part { get; set; }
        public bool Deleted { get; set; }
        public ICollection<Fungicide_X_Pompon_Part> Fungicide_X_Pompon_Parts { get; set; }
        public Pompon_Parts()
        {
            Fungicide_X_Pompon_Parts = new List<Fungicide_X_Pompon_Part>();
        }
    }
}
