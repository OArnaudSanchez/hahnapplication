using Hahn.ApplicatonProcess.July2021.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> AddUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
    }
}
