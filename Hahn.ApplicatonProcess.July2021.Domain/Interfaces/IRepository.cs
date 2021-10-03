using Hahn.ApplicatonProcess.July2021.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(T Entity);
        Task<bool> Update(T Entity);
        Task<bool> Delete(int id);
    }
}
