using CarRental.DAL.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.DAL.Repositories
{
    public class ReservationRepository : RepositoryBase<Reservation>, IReservationRepository
    {
        private readonly ApplicationDbContext context;
        public ReservationRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> CarCanBeReservedAsync(Reservation reservation)
        {
            List<Reservation> entities = await FilterReservations(reservation);
            bool isCorrect = IsRentalDateCorrect(reservation);
            return entities.Count == 0 && isCorrect ? true : false;
        }

        public async Task<bool> CarCanBeUpdatedAsync(Reservation reservation)
        {
            List<Reservation> entities = await FilterReservations(reservation);
            int count = entities.Count;
            int id = count == 0 ? 0 : entities.FirstOrDefault().ReservationId;
            bool isCorrect = IsRentalDateCorrect(reservation);
            if (isCorrect && (count == 0 || (count == 1 && id == reservation.ReservationId)))
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
        private bool IsRentalDateCorrect(Reservation reservation)
        {
            return reservation.RentalDate > DateTime.Now && reservation.RentalDate < reservation.ReturnDate;
        }
    }
}
