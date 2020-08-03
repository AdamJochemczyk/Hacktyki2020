using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories
{
    public class LocationRepository : RepositoryBase<Location>, ILocationRepository
    {
        public LocationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Location> GetActualLocationByReservationIdAsync(int id)
        {
            var entity = await context.Locations
                .Where(p => p.ReservationId == id)
                .Where(p => p.IsActual == true)
                .SingleOrDefaultAsync();
            return entity;
        }
    }
}
