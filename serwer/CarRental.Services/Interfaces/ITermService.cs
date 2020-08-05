using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
    public interface ITermService
    {
        Task<IEnumerable<string>> GetFreeTermsByCarIdAsync(int id, DateTime? rentalDate, DateTime? returnDate);
    }
}
