using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IMapper mapper;
        public ReservationService(IReservationRepository reservationRepository,
            IMapper mapper)
        {
            this.reservationRepository = reservationRepository;
            this.mapper = mapper;
        }

        public async Task<ReservationDto> CreateReservationAsync(ReservationCreateDto reservationCreateDto)
        {
            var reservation = new Reservation()
            {
                UserId = reservationCreateDto.UserId,
                CarId = reservationCreateDto.CarId,
                RentalDate = reservationCreateDto.RentalDate,
                ReturnDate = reservationCreateDto.ReturnDate,
                IsFinished = false,
                DateCreated = DateTime.Now
            };
            reservationRepository.Create(reservation);
            await reservationRepository.SaveChangesAsync();
            reservation = await reservationRepository.FindByIdAsync(reservation.ReservationId);
            return mapper.Map<ReservationDto>(reservation);
        }

        public async Task DeleteReservationAsync(int id)
        {
            await reservationRepository.Delete(id);
            await reservationRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
        {
            var reservations = await reservationRepository.FindAllAsync();
            return mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task<ReservationDto> GetReservationByIdAsync(int id)
        {
            var reservation = await reservationRepository.FindByIdAsync(id);
            return mapper.Map<ReservationDto>(reservation);
        }

        public async Task<ReservationDto> UpdateReservationAsync(ReservationUpdateDto reservationUpdateDto)
        {
            var reservation = await reservationRepository.FindByIdAsync(reservationUpdateDto.ReservationId);
            reservation.Update(reservationUpdateDto.RentalDate,
                reservationUpdateDto.ReturnDate,
                reservationUpdateDto.IsFinished);
            reservationRepository.Update(reservation);
            await reservationRepository.SaveChangesAsync();
            reservation = await reservationRepository.FindByIdAsync(reservationUpdateDto.ReservationId);
            return mapper.Map<ReservationDto>(reservation);
        }

        public async Task<bool> ReservationCanBeCreatedAsync(ReservationCreateDto reservationDto)
        {
            var reservation = new Reservation()
            {
                RentalDate = reservationDto.RentalDate,
                ReturnDate = reservationDto.ReturnDate,
                CarId = reservationDto.CarId
            };
            List<Reservation> reservations = await reservationRepository.FilterReservationsAsync(reservation);
            return reservations.Count == 0 ? true : false;
        }

        public async Task<bool> ReservationCanBeUpdatedAsync(ReservationUpdateDto reservationDto)
        {
            var reservation = new Reservation()
            {
                ReservationId = reservationDto.ReservationId,
                RentalDate = Convert.ToDateTime(reservationDto.RentalDate),
                ReturnDate = Convert.ToDateTime(reservationDto.ReturnDate),
                CarId = reservationDto.CarId
            };
            List<Reservation> reservations = await reservationRepository.FilterReservationsAsync(reservation);
            int count = reservations.Count;
            int id = count == 0 ? 0 : reservations.FirstOrDefault().ReservationId;
            return (count == 0 || (count == 1 && id == reservation.ReservationId)) ? true : false;
        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservationsByUserIdAsync(int id)
        {
            var reservations = await reservationRepository.FindAllByUserIdAsync(id);
            return mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task<IEnumerable<ReservationDto>> GetActualReservationsByCarIdAsync(int id)
        {
            var reservations = await reservationRepository.FindAllByCarIdAsync(id);
            return mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }    
    }
}
