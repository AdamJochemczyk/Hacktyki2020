using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>,IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<User> FindByIdDetails(int id)
        {
              return await context.Set<User>().Where(e=>e.isDeleted==false).FirstOrDefaultAsync(e => e.UserId == id);    
        }
        public async Task<IEnumerable<User>> FindAllUsers()
        {
            return await context.Set<User>().Where(e => e.isDeleted == false).ToListAsync();
        }
        public async Task<User> FindByCodeOfVerification(string code)
        {
            return await context.Set<User>().FirstOrDefaultAsync(e => e.CodeOfVerification == code);
        }

        public async Task<User> FindByLogin(string email )
        {
            return await context.Set<User>().Where(e => e.isDeleted == false).FirstOrDefaultAsync(e => e.Email == email);
        }

    } 
}
