using Microsoft.AspNetCore.Mvc;
using Pomponet.Model;
using Pomponet.Services;

namespace Pomponet.Controllers
{
    [Route("api[Controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<People>>> GetAll()
        {
            var people = await _peopleService.GetAll();
            var peopleNotEliminated = people.Where(p => !p.Deleted).ToList();
            return Ok(await _peopleService.GetAll());
        }
        [HttpGet("{Id_Person}")]

        public async Task<ActionResult<People>> GetPeople(int Id_Person)
        {
            var Peopl = await _peopleService.GetPeople(Id_Person);
            if (Peopl == null)
            {
                return BadRequest("Registro no encontrado");
            }
            if (Peopl.Deleted == true)
            {
                return BadRequest("Regsitro no valido");
            }
            return Ok(Peopl);
        }

        [HttpPost]
        public async Task<ActionResult<People>> CreatePeople(string Names, string Email, string UserName, string Password, int Age)
        {
            var Createpeople = await _peopleService.CreatePeople(Names, Email, UserName, Password, Age);
            if (Createpeople != null)
            {
                return Ok(Createpeople);
            }
            else
            {
                return BadRequest("Error al insertar dato");
            }
        }

        [HttpPut("{Id_Person}")]
        public async Task<ActionResult<People>> UpdatePeople(int Id_Person, string Names, string Email, string UserName, string Password, int Age)
        {
            var Updatepeople = await _peopleService.UpdatePeople(Id_Person, Names, Email, UserName, Password, Age);
            if (Updatepeople != null)
            {
                return Ok(Updatepeople);
            }
            else
            {
                return BadRequest("Error en la base de datos");
            }
        }
        [HttpDelete("Delete/{Id_Person}")]
        public async Task<ActionResult<People>> DeletePeople(int Id_Person)
        {
            var peopleToDelete = await _peopleService.DeletePeople(Id_Person);
            if (peopleToDelete != null)
            {
                return Ok(peopleToDelete);
            }
            else
            {
                return BadRequest("Error al actualizar la base de datos :(");
            }
        }
        [HttpPost("Login")]
        public async Task<ActionResult<bool>> Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return BadRequest("El nombre de usuario y la contraseña son obligatorios.");
            }

            var user = await _peopleService.Login(userName, password);
            if (user != null)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }
}