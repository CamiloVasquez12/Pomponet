using System.ComponentModel.DataAnnotations;
namespace Pomponet.Model
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
    }
}
