using System.ComponentModel.DataAnnotations;

namespace PomponetWebsite.Models
{
    public class Players
    {
        [Key]
        public int Id_Player { get; set; }
        public required int Score { get; set; }
        public required int Id_Person { get; set; }
        public People? People { get; set; }
        public bool Deleted { get; set; }
        public ICollection<Money> Money { get; set; }
        public Players()
        {
            Money = new List<Money>();
            Player_Achievements = new List<Player_Achievements>();
        }
        public ICollection<Player_Achievements> Player_Achievements { get; set; }
    }
}
