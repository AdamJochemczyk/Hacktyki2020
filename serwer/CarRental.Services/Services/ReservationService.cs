using AutoMapper;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Reservation;
using Domain.Models;
using System;
using System.Collections.Generic;
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
                ReturnDate = reservationCreateDto.ReturnDate
            };
            repository.Create(entity);
            await repository.SaveChangesAsync();
            entity = await repository.FindByIdAsync(entity.ReservationId);
            return mapper.Map<ReservationDto>(entity);
        }

        public async Task DeleteReservationAsync(int id)
        {
            var entity = await repository.FindByIdAsync(id);
            repository.Delete(entity);
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
            entity.Update(reservationUpdateDto.RentalDate, reservationUpdateDto.ReturnDate, reservationUpdateDto.CarId, reservationUpdateDto.UserId);
            repository.Update(entity);
            await repository.SaveChangesAsync();
            entity = await repository.FindByIdAsync(reservationUpdateDto.ReservationId);
            return mapper.Map<ReservationDto>(entity);
        }
    }
}
