using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.DAL.Repositories
{
    public class ReservationRepository : RepositoryBase<Reservation>, IReservationRepository
    {
        public ReservationRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<bool> ReservationCanBeCreatedAsync(Reservation reservation)
        {
            List<Reservation> entities = await FilterReservations(reservation);
            return entities.Count == 0 ? true : false;
        }

        public async Task<bool> ReservationCanBeUpdatedAsync(Reservation reservation)
        {
            List<Reservation> entities = await FilterReservations(reservation);
            int count = entities.Count;
            int id = count == 0 ? 0 : entities.FirstOrDefault().ReservationId;
            if (count == 0 || (count == 1 && id == reservation.ReservationId))
            {
                return true;
            }
            return false;
        }

        private async Task<List<Reservation>> FilterReservations(Reservation reservation)
        {
            return await context.Reservations
                .Where(p => p.CarId == reservation.CarId)
                .Where(p => p.IsFinished == false)
                .Where(p =>
                (p.RentalDate < reservation.ReturnDate && p.ReturnDate > reservation.RentalDate)
                || (p.RentalDate < reservation.RentalDate && p.ReturnDate > reservation.RentalDate)
                || (p.RentalDate >= reservation.RentalDate && p.ReturnDate <= reservation.ReturnDate))
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> FindAllByUserIdAsync(int userId)
        {
            var result = await context.Reservations
                .Where(p => p.UserId == userId)
                .ToListAsync();
            return result;
        }
    }
}
