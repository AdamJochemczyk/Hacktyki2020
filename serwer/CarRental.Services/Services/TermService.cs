using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class TermService : ITermService
    {
        private readonly IReservationRepository reservationRepository;
        public TermService(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<string>> GetFreeTermsByCarIdAsync(int id, DateTime? rentalDate, DateTime? returnDate)
        {
            var reservations = await reservationRepository.FindAllByCarIdAsync(id);
            List<int> freeDays = PrepareFreeDaysArray(rentalDate, returnDate);
            freeDays = RemoveUnavailableDates(reservations, freeDays);
            return GetConvertedDates(freeDays);
        }

        public List<int> PrepareFreeDaysArray(DateTime? rentalDate, DateTime? returnDate)
        {
            int week = 7;
            var startRange
                = (rentalDate == null || rentalDate.Value.DayOfYear - week < DateTime.Now.DayOfYear) ?
                DateTime.Now.DayOfYear : rentalDate.Value.DayOfYear - week;
            var endRange = returnDate == null ? 2 * week : returnDate.Value.DayOfYear - rentalDate.Value.DayOfYear + week + 1;
            var freeDays = Enumerable.Range(startRange, endRange).ToList();
            return freeDays;
        }

        public List<int> RemoveUnavailableDates(IEnumerable<Reservation> reservations, List<int> freeDays)
        {
            foreach (var reservation in reservations)
            {
                for (int i = reservation.RentalDate.DayOfYear; i <= reservation.ReturnDate.DayOfYear; i++)
                {
                    freeDays.Remove(i);
                }
            }
            return freeDays;
        }

        public IEnumerable<string> GetConvertedDates(List<int> freeDays)
        {
            var dates = new List<string>();
            foreach (var dayOfYear in freeDays)
            {
                var date = new DateTime(DateTime.Now.Year, 1, 1).AddDays(dayOfYear - 1).Date.ToString("dd/MM/yyyy");
                dates.Add(date);
            }
            return dates;
        }

        public bool DatesAreCorrect(DateTime rentalDate, DateTime returnDate)
        {
            return rentalDate < returnDate && rentalDate.Date >= DateTime.Now.Date;
        }

        public bool DatesHaveValue(DateTime? rentalDate, DateTime? returnDate)
        {
            return rentalDate.HasValue && returnDate.HasValue;
        }
    }
}
