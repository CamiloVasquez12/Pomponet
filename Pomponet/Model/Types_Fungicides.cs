using System.ComponentModel.DataAnnotations;
namespace Pomponet.Model
{
    public class Types_Fungicides
    {
        [Key]
        public int Id_Type_Fungicide { get; set; }
        public required string Type_Fungicide { get; set; }
        public required int Id_Funicides { get; set; }
        public Fungicides? Fungicides {  get; set; }
        public bool Deleted { get; set; }

    }
}
