using CarRental.Services.Models.Location;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
    public interface ILocationService
    {
        Task<LocationDto> GetActualLocationByReservationIdAsync(int id);
        Task<LocationDto> CreateLocationAsync(LocationCreateDto locationCreateDto);
        Task DeleteLocationAsync(int id);
        
    }
}
