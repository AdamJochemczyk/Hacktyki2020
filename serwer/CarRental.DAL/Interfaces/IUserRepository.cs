using CarRental.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.DAL.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> FindByIdDetails(int id);
        Task<IEnumerable<User>> FindAllUsers();
        Task<User> FindByCodeOfVerification(string code);
        Task<User> FindByLogin(string email);
    }
}
