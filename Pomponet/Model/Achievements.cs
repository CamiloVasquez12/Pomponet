using System.ComponentModel.DataAnnotations;
namespace Pomponet.Model
{
    public class Achievements
    {
        [Key]
        public int Id_Achievement { get; set; }
        public required string Achievement { get; set; }
        public bool Deleted { get; set; }
    }
}
