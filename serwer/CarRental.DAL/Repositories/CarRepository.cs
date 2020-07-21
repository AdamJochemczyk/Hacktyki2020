using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.DAL.Repositories
{
    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
