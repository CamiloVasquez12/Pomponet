using System.ComponentModel.DataAnnotations;

namespace PomponetWebsite.Models
{
    public class Types_Fungicides
    {
        [Key]
        public int Id_Type_Fungicide { get; set; }

        [Required]
        public string Type_Fungicide { get; set; }

        [Required]
        public int Id_Funicides { get; set; }

        public Fungicides? Fungicides { get; set; }

        public bool Deleted { get; set; }
    }
}
