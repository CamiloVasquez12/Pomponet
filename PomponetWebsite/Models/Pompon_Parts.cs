using System.ComponentModel.DataAnnotations;
namespace PomponetWebsite.Models
{
    public class Pompon_Parts
    {
        [Key]
        public int Id_Pompon_Part { get; set; }
        public required string Part { get; set; }
        public bool Deleted { get; set; }
    }
}
