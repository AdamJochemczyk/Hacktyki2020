using CarRental.DAL.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.DAL.Repositories
{
    public class ReservationRepository : RepositoryBase<Reservation>, IReservationRepository
    {
        public ReservationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
