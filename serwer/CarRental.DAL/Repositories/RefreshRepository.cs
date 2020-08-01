using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.DAL.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Repositories
{
    public class RefreshRepository : RepositoryBase<RefreshToken> , IRefreshRepository
    {
        public RefreshRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.context = dbContext;
        }

        public  async Task<RefreshToken> FindByRefreshToken(string refresh)
        {
            return await context.Set<RefreshToken>().FirstOrDefaultAsync(e => e.Refresh == refresh);
        }
    }
}
