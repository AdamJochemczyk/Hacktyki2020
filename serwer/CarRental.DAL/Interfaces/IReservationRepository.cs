using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Interfaces
{
    public interface IReservationRepository : IRepositoryBase<Reservation>
    {
        Task<bool> CarCanBeReservedAsync(Reservation reservation);
        Task<bool> CarCanBeUpdatedAsync(Reservation reservation);
    }
}
