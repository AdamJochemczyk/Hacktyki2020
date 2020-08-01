using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Interfaces
{
   public interface IRefreshRepository:IRepositoryBase<RefreshToken>
    {
        public  Task<RefreshToken> FindByRefreshToken(string refresh);
    }
}
