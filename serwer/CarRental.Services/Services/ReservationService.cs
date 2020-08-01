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
            entity.Update(reservationUpdateDto.RentalDate, reservationUpdateDto.ReturnDate, reservationUpdateDto.IsFinished);
            repository.Update(entity);
            await repository.SaveChangesAsync();
            entity = await repository.FindByIdAsync(reservationUpdateDto.ReservationId);
            return mapper.Map<ReservationDto>(entity);
        }

        public async Task<bool> ReservationCanBeCreatedAsync(ReservationCreateDto reservationDto)
        {
            var entity = new Reservation()
            {
                RentalDate = reservationDto.RentalDate,
                ReturnDate = reservationDto.ReturnDate,
                CarId = reservationDto.CarId
            };
            return await repository.ReservationCanBeCreatedAsync(entity);
        }

        public async Task<bool> ReservationCanBeUpdatedAsync(ReservationUpdateDto reservationDto)
        {
            var entity = new Reservation()
            {
                ReservationId = reservationDto.ReservationId,
                RentalDate = reservationDto.RentalDate,
                ReturnDate = reservationDto.ReturnDate,
                CarId = reservationDto.CarId
            };
            return await repository.ReservationCanBeUpdatedAsync(entity);
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

        public async Task<IEnumerable<string>> GetFreeTermsByCarIdAsync(int id)
        {
            var reservations = await repository.FindCloseReservationsByCarIdAsync(id);
            var freeDays = Enumerable.Range(DateTime.Now.DayOfYear, 14).ToList();
            var dates = new List<string>();
            string date;
            foreach (var reservation in reservations)
            {
                for (int i = reservation.RentalDate.DayOfYear; i <= reservation.ReturnDate.DayOfYear; i++)
                {
                    freeDays.Remove(i);
                }
            }

            foreach (var dayOfYear in freeDays)
            {
                date = new DateTime(DateTime.Now.Year, 1, 1).AddDays(dayOfYear - 1).Date.ToString("dd/MM/yyyy");
                dates.Add(date);
            }
            return dates;
        }
    }
}
