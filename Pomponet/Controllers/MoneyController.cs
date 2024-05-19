using Microsoft.AspNetCore.Mvc;
using Pomponet.Model;
using Pomponet.Services;

namespace Pomponet.Controllers
{
    [Route("api[Controller]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        private readonly IMoneyService _moneyService;

        public MoneyController(IMoneyService moneyService)
        {
            _moneyService = moneyService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Money>>> GetAll()
        {
            var money = await _moneyService.GetAll();
            var moneyNotEliminated = money.Where(m => !m.Deleted).ToList();
            return Ok(await _moneyService.GetAll());
        }
        [HttpGet("{Id_Money}")]

        public async Task<ActionResult<Money>> getMone(int Id_Money)
        {
            var Mone = await _moneyService.GetMoney(Id_Money);
            if (Mone == null)
            {
                return BadRequest("Registro no encontrado");
            }
            if (Mone.Deleted == true)
            {
                return BadRequest("Regsitro no valido");
            }
            return Ok(Mone);
        }

        [HttpPost]
        public async Task<ActionResult<Money>> CreateMoney(int Quantity, int Id_Player)
        {
            var Createmoney = await _moneyService.CreateMoney(Quantity, Id_Player);
            if (Createmoney != null)
            {
                return Ok(Createmoney);
            }
            else
            {
                return BadRequest("Error al insertar dato");
            }
        }

        [HttpPut("{Id_Money}")]
        public async Task<ActionResult<Money>> UpdateMoney(int Id_Money, int Quantity, int Id_Player)
        {
            var Updatemoney = await _moneyService.UpdateMoney(Id_Money, Quantity, Id_Player);
            if (Updatemoney != null)
            {
                return Ok(Updatemoney);
            }
            else
            {
                return BadRequest("Error en la base de datos");
            }
        }
        [HttpDelete("Delete/{Id_Money}")]
        public async Task<ActionResult<Money>> DeleteMoney(int Id_Money)
        {
            var moneyToDelete = await _moneyService.DeleteMoney(Id_Money);
            if (moneyToDelete != null)
            {
                return Ok(moneyToDelete);
            }
            else
            {
                return BadRequest("Error al actualizar la base de datos :(");
            }
        }
    }
}