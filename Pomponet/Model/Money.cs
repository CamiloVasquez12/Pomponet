using System.ComponentModel.DataAnnotations;
namespace Pomponet.Model
{
    public class Money
    {
        [Key]
        public int Id_Money{ get; set; }
        public required int Quantity { get; set; }
        public required int Id_Player { get; set; }
        public Players? Players { get; set; }
        public bool Deleted { get; set; }
    }
}
