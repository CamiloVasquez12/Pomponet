using System.ComponentModel.DataAnnotations;
namespace PomponetWebsite.Models
{
    public class Player_Achievements
    {
        [Key]
        public int Id_Player_Achievement { get; set; }
        public required int Id_Achievement { get; set; }
        public Achievements? Achievements { get; set; }
        public required int Logros_Totales { get; set; }
        public required int Id_Player {  get; set; }
        public Players? Players { get; set; }
        public bool Deleted { get; set; }
    }
}
