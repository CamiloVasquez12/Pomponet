using Microsoft.AspNetCore.Mvc;
using Pomponet.Model;
using Pomponet.Services;

namespace Pomponet.Controllers
{
    [Route("api[Controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly IInventoriesService _inventoriesService;

        public InventoriesController(IInventoriesService inventoriesService)
        {
            _inventoriesService = inventoriesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Inventories>>> GetAll()
        {
            var inventories = await _inventoriesService.GetAll();
            var inventoriesNotEliminated = inventories.Where(i => !i.Deleted).ToList();
            return Ok(await _inventoriesService.GetAll());
        }
        [HttpGet("{Id_Inventory}")]

        public async Task<ActionResult<Inventories>> getInventory(int Id_Inventory)
        {
            var Inventory = await _inventoriesService.GetInventories(Id_Inventory);
            if (Inventory == null)
            {
                return BadRequest("Registro no encontrado");
            }
            if (Inventory.Deleted == true)
            {
                return BadRequest("Regsitro no valido");
            }
            return Ok(Inventory);
        }

        [HttpPost]
        public async Task<ActionResult<Inventories>> CreateInventories(int Number_Inventorie, int Id_Person, int Id_Tool, int Id_Epp)
        {
            var Createinventories = await _inventoriesService.CreateInventories(Number_Inventorie, Id_Person, Id_Tool, Id_Epp);
            if (Createinventories != null)
            {
                return Ok(Createinventories);
            }
            else
            {
                return BadRequest("Error al insertar dato");
            }
        }

        [HttpPut("{Id_Inventory}")]
        public async Task<ActionResult<Inventories>> UpdateInventories(int Id_Inventory, int Number_Inventorie, int Id_Person, int Id_Tool, int Id_Epp)
        {
            var Updateinventories = await _inventoriesService.UpdateInventories(Id_Inventory, Number_Inventorie, Id_Person, Id_Tool, Id_Epp);
            if (Updateinventories != null)
            {
                return Ok(Updateinventories);
            }
            else
            {
                return BadRequest("Error en la base de datos");
            }
        }
        [HttpDelete("Delete/{Id_Inventory}")]
        public async Task<ActionResult<Inventories>> DeleteInventories(int Id_Inventory)
        {
            var InventorieToDelete = await _inventoriesService.DeleteInventories(Id_Inventory);
            if (InventorieToDelete != null)
            {
                return Ok(InventorieToDelete);
            }
            else
            {
                return BadRequest("Error al actualizar la base de datos :(");
            }
        }
    }
}