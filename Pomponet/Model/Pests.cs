using System.ComponentModel.DataAnnotations;
namespace Pomponet.Model
{
    public class Pests
    {
        [Key]
        public int Id_Pest { get; set; }
        public required string Pest { get; set; }
        public bool Deleted { get; set; }
    }
}
