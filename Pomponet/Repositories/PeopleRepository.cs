using Pomponet.Context;
using Pomponet.Model;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Pomponet.Repositories
{
    public interface IPeopleRepository
    {
        Task<List<People>> GetAll();
        Task<People> GetPeople(int Id_Person);
        Task<People> CreatePeople(string Names, string Email, string UserName, string Password, int Age);
        Task<People> UpdatePeople(People people);
        Task<People> DeletePeople(People people);
        Task<People> Login(string userName, string password);
    }
    public class PeopleRepository : IPeopleRepository
    {
        private readonly CropsDbContext _db;
        public PeopleRepository(CropsDbContext db)
        {
            _db = db;
        }
        public async Task<People> CreatePeople(string names, string email, string username, string password, int age)
        {
            People newPeople = new People
            {
                Names = names,
                Email = email,
                UserName = username,
                Password = password,
                Age = age,
            };
            await _db.People.AddAsync(newPeople);
            _db.SaveChanges();
            return newPeople;
        }
        public async Task<List<People>> GetAll()
        {
            return await _db.People.ToListAsync();
        }
        public async Task<People> GetPeople(int Id_Person)
        {
            return await _db.People.FirstOrDefaultAsync(p => p.Id_Person == Id_Person);
        }
        public async Task<People> UpdatePeople(People people)
        {
            People ConsultUpdate = await _db.People.FindAsync(people.Id_Person);
            if (ConsultUpdate != null)
            {
                ConsultUpdate.Names = people.Names;
                ConsultUpdate.Email = people.Email;
                ConsultUpdate.UserName = people.UserName;
                ConsultUpdate.Password = people.Password;
                ConsultUpdate.Age = people.Age;
                await _db.SaveChangesAsync();
            }
            return ConsultUpdate;
            throw new NotImplementedException();
        }
        public async Task<People> DeletePeople(People people)
        {
            _db.People.Attach(people);
            _db.Entry(people).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return people;
        }
        public async Task<People> Login(string userName, string password)
        {
            return await _db.People.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);

        }
    }
}
