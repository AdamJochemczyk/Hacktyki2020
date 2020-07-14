using CarRental.Services.Models.Reservation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
    public interface IReservationService 
    {
        Task<ReservationDto> CreateReservationAsync(ReservationCreateDto reservationDto);
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync();
        Task<ReservationDto> GetReservationByIdAsync(int id);
        Task<ReservationDto> UpdateReservationAsync(ReservationUpdateDto reservationDto);
        Task DeleteReservationAsync(int id);
        Task<bool> CarCanBeReservedAsync(ReservationCreateDto reservationDto);
        Task<bool> CarCanBeUpdatedAsync(ReservationUpdateDto reservationDto);
    }
}
