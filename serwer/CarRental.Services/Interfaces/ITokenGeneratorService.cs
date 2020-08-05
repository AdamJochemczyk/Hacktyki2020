using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
   public interface ITokenGeneratorService
    {
         Task<string> GenerateToken(int userId);
         string RefreshGenerateToken();
    }
}
