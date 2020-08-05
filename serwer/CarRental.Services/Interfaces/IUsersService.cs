using CarRental.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<UsersDto>> GetAllUsers();
        Task<UsersDto> GetUser(int Id);
        Task<bool> DeleteUser(int Id);
        Task<UsersDto> UpdateUser(UsersDto Id);
    }
}
