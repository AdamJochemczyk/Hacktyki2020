using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Interfaces
{
    public interface IReservationRepository : IRepositoryBase<Reservation>
    {
        Task<IEnumerable<Reservation>> FindAllByUserIdAsync(int id);
        Task<IEnumerable<Reservation>> FindAllByCarIdAsync(int id);
        Task<List<Reservation>> FilterReservations(Reservation reservation);
        //Task<bool> ReservationCanBeCreatedAsync(Reservation reservation);
        //Task<bool> ReservationCanBeUpdatedAsync(Reservation reservation);
    }
}
