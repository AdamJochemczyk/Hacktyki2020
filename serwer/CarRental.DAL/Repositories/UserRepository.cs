﻿using CarRental.DAL.Entities;
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
              return await context.Set<User>().FirstOrDefaultAsync(e => e.UserId == id);    
        }
        public async Task<User> VerificateUser(string email,string password )
        {
            return await context.Set<User>().FirstOrDefaultAsync(e => e.Email == email&&e.EncodePassword==password);
        }
    }
}
