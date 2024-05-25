using System.ComponentModel.DataAnnotations;
namespace PomponetWebsite.Models   
{
    public class People
    {
        [Key]
        public int Id_Person { get; set; }
        public required string Names { get; set; }
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required int Age { get; set; }
        public bool Deleted { get; set; }
        public ICollection<Inventories> Inventories { get; set; }
        public People()
        {
            Inventories = new List<Inventories>();
            Players = new List<Players>();
        }
        public ICollection<Players> Players { get; set; }
    }
}
