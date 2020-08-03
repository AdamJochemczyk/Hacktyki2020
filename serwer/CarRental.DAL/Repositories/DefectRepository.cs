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
   public class DefectRepository:RepositoryBase<Defect>,IDefectRepository
    {
        public DefectRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Defect> FindDefectById(int id)
        {
            return await context.Set<Defect>().FirstOrDefaultAsync(e => e.DefectId == id);
        }

    }
}
