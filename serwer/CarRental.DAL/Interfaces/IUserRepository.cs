using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> FindByIdDetails(int id);
        Task<User> FindByLogin(string email);
    }
}
