using System.ComponentModel.DataAnnotations;
namespace PomponetWebsite.Models
{
    public class AplicationTools
    {
        [Key]
        public int Id_AplicationTool { get; set; }
        public required string Tool { get; set; }
        public required string Quantity { get; set; }
        public required string Description { get; set; }
        public required int Price { get; set; }
        public bool Deleted { get; set; }
    }
}