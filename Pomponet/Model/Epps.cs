using System.ComponentModel.DataAnnotations;
namespace Pomponet.Model
{
    public class Epps
    {
        [Key]
        public int Id_Epp { get; set; }
        public required string Name_Epp { get; set; }
        public bool Deleted { get; set; }

    }
}