using Pomponet.Context;
using Pomponet.Model;
using Pomponet.Repositories;

namespace Pomponet.Services
{
    public interface IPeopleService
    {
        Task<List<People>> GetAll();
        Task<People> GetPeople(int Id_Person);
        Task<People> CreatePeople(string Names, string Email, string UserName, string Password, int Age);
        Task<People> UpdatePeople(int Id_Person, string? Names = null, string? Email = null, string? UserName = null, string? Password = null, int? Age = null);
        Task<People> DeletePeople(int Id_Person);
        Task<People> Login(string userName, string password);
    }
    public class PeopleService : IPeopleService
    {
        private readonly IPeopleRepository _peopleRepository;
        public PeopleService(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }
        public async Task<People> CreatePeople(string names, string email, string username, string password, int age)
        {
            return await _peopleRepository.CreatePeople(names, email, username, password, age);
        }
        public async Task<List<People>> GetAll()
        {
            return await _peopleRepository.GetAll();
        }
        public async Task<People> GetPeople(int Id_Person)
        {
            return await _peopleRepository.GetPeople(Id_Person);
        }
        public async Task<People> UpdatePeople(int Id_Person, string? Names = null, string? Email = null, string? UserName = null, string? Password = null, int? Age = null)
        {
            People newPeople= await _peopleRepository.GetPeople(Id_Person);
            if (newPeople!= null)
            {
                if (Names != null)
                {
                    newPeople.Names = Names;
                }
                if (Email != null)
                {
                    newPeople.Email = Email;
                }
                if (UserName != null)
                {
                    newPeople.UserName = UserName;
                }
                if (Password != null)
                {
                    newPeople.Password = Password;
                }
                if (Age != null)
                {
                    newPeople.Age = (int)Age;
                }
                return await _peopleRepository.UpdatePeople(newPeople);
            }
            throw new NotImplementedException("Registro no encontrado");
        }
        public async Task<People> DeletePeople(int Id_Person)
        {
            People people = await _peopleRepository.GetPeople(Id_Person);
            if (people == null)
            {
                throw new Exception($"This Person with the id {people} don't exist. ");
            }
            people.Deleted = true;

            return await _peopleRepository.DeletePeople(people);
        }
        public async Task<People> Login(string userName, string password)
        {
            return await _peopleRepository.Login(userName, password);
        }
    }
}
