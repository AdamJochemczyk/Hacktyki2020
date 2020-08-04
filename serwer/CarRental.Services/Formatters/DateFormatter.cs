using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Services.Formatters
{
    class DateFormatter : IValueConverter<DateTime, string>
    {
        public string Convert(DateTime sourceMember, ResolutionContext context)
            => sourceMember.Date.ToString("yyyy-MM-dd");
    }
}
