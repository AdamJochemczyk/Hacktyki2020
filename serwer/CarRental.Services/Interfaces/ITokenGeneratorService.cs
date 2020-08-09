using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
   public interface ITokenGeneratorService
    {
         string GenerateToken(User user);
         string RefreshGenerateToken();
    }
}
