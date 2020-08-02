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

        public new async Task<IEnumerable<Reservation>> FindAllAsync()
        {
            var result = await context
                .Reservations
                .Include(p => p.Car)
                .Include(p => p.User)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Reservation>> FindAllByUserIdAsync(int id)
        {
            var result = await context.Reservations
                .Where(p => p.UserId == id)
                .Include(p => p.Car)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Reservation>> FindAllByCarIdAsync(int id)
        {
            var result = await context.Reservations
                .Where(p => p.IsFinished == false)
                .Where(p => p.CarId == id)
                .Include(p => p.Car)
                .ToListAsync();
            return result;
        }

        public new async Task<Reservation> FindByIdAsync(int id)
        {
            var result = await context.Reservations
                .Where(p => p.ReservationId == id)
                .Include(p => p.Car)
                .SingleOrDefaultAsync();
            return result;
        }

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

        //public async Task<IEnumerable<Reservation>> FindCloseReservationsByCarIdAsync(int id)
        //{
        //    return await context.Reservations
        //        .Where(p => p.CarId == id)
        //        .Where(p => p.IsFinished == false)
        //      //  .Where(p => p.RentalDate <= DateTime.Now.AddDays(14).Date)
        //       // .Where(p => p.RentalDate >= DateTime.Now.Date)
        //        .ToListAsync();
        //}
    }
}
