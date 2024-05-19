using System.ComponentModel.DataAnnotations;
namespace Pomponet.Model
{
    public class Fungicide_X_Pompon_Part
    {
        [Key]
        public int Id_Fungicide_X_Pompon_Part { get; set; }
        public required int Id_Pompon_Part { get; set; }
        public Pompon_Parts? Pompon_Parts { get; set; }
        public required int Id_Fungicide {  get; set; }
        public Fungicides? Fungicides {  get; set; }
        public bool Deleted { get; set; }
    }
}
