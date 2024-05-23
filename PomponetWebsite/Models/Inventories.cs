using System.ComponentModel.DataAnnotations;
namespace PomponetWebsite.Models
{
    public class Inventories
    {
        [Key]
        public int Id_Inventory { get; set; }
        public required int Number_Inventorie { get; set; }
        public required int Id_Person { get; set; }
        public People? People { get; set; }
        public required int Id_Tool { get; set; }
        public AplicationTools? Aplication_Tools { get; set; }
        public required int Id_Epp {  get; set; }
        public Epps? Epps { get; set; }
        public bool Deleted { get; set; }
    }
}
