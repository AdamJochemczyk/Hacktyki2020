using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Interfaces
{
    public interface ICarRepository : IRepositoryBase<Car>
    {
        Task<IEnumerable<Car>> GetAvailableCars(DateTime rentalDate, DateTime returnDate);
    }
}
