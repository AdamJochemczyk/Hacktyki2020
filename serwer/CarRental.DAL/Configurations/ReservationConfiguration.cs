using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.DAL.Configurations
{
    class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(p => p.RentalDate)
                .IsRequired();

            builder.Property(p => p.ReturnDate)
                .IsRequired();

            builder.Property(p => p.CarId)
                .IsRequired();

            builder.Property(p => p.UserId)
                .IsRequired();
        }
    }
}
