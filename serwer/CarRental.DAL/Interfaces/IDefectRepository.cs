using CarRental.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.DAL.Interfaces
{
   public interface IDefectRepository: IRepositoryBase<Defect>
    {
        Task<IEnumerable<Defect>> FindAllDefects();
        Task<Defect> FindDefectById(int id);
    }
}
