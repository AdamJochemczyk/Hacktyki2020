using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository repository;
        private readonly IMapper mapper;
        public ReservationService(IReservationRepository repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ReservationDto> CreateReservationAsync(ReservationCreateDto reservationCreateDto)
        {
            var entity = new Reservation()
            {
                UserId = reservationCreateDto.UserId,
                CarId = reservationCreateDto.CarId,
                RentalDate = reservationCreateDto.RentalDate,
                ReturnDate = reservationCreateDto.ReturnDate,
                IsFinished = false,
                DateCreated = DateTime.Now
            };
            repository.Create(entity);
            await repository.SaveChangesAsync();
            entity = await repository.FindByIdAsync(entity.ReservationId);
            return mapper.Map<ReservationDto>(entity);
        }

        public async Task DeleteReservationAsync(int id)
        {
            await repository.Delete(id);
            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
        {
            var entities = await repository.FindAllAsync();
            return mapper.Map<IEnumerable<ReservationDto>>(entities);
        }

        public async Task<ReservationDto> GetReservationByIdAsync(int id)
        {
            var entity = await repository.FindByIdAsync(id);
            return mapper.Map<ReservationDto>(entity);
        }

        public async Task<ReservationDto> UpdateReservationAsync(ReservationUpdateDto reservationUpdateDto)
        {
            var entity = await repository.FindByIdAsync(reservationUpdateDto.ReservationId);
            entity.Update(reservationUpdateDto.RentalDate,
                reservationUpdateDto.ReturnDate,
                reservationUpdateDto.IsFinished);
            repository.Update(entity);
            await repository.SaveChangesAsync();
            entity = await repository.FindByIdAsync(reservationUpdateDto.ReservationId);
            return mapper.Map<ReservationDto>(entity);
        }

        public async Task<bool> ReservationCanBeCreatedAsync(ReservationCreateDto reservationDto)
        {
            var reservation = new Reservation()
            {
                RentalDate = reservationDto.RentalDate,
                ReturnDate = reservationDto.ReturnDate,
                CarId = reservationDto.CarId
            };
            List<Reservation> entities = await repository.FilterReservationsAsync(reservation);
            return entities.Count == 0 ? true : false;
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
            List<Reservation> entities = await repository.FilterReservationsAsync(reservation);
            int count = entities.Count;
            int id = count == 0 ? 0 : entities.FirstOrDefault().ReservationId;
            return (count == 0 || (count == 1 && id == reservation.ReservationId)) ? true : false;
        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservationsByUserIdAsync(int id)
        {
            var entities = await repository.FindAllByUserIdAsync(id);
            return mapper.Map<IEnumerable<ReservationDto>>(entities);
        }

        public async Task<IEnumerable<ReservationDto>> GetActualReservationsByCarIdAsync(int id)
        {
            var entities = await repository.FindAllByCarIdAsync(id);
            return mapper.Map<IEnumerable<ReservationDto>>(entities);
        }    
    }
}
