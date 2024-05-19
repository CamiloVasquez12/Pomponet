using System.ComponentModel.DataAnnotations;
namespace Pomponet.Model
{
    public class Players
    {
        [Key]
        public int Id_Player{ get; set; }
        public required int Score { get; set; }
        public required int Id_Person { get; set; }
        public People? People { get; set; }
        public bool Deleted { get; set; }
    }
}
