using System.ComponentModel.DataAnnotations;
namespace PomponetWebsite.Models
{
    public class Achievements
    {
        [Key]
        public int Id_Achievement { get; set; }
        public required string Achievement { get; set; }
        public bool Deleted { get; set; }
    }
}
