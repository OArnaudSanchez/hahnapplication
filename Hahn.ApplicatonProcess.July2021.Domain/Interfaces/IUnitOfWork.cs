using Hahn.ApplicatonProcess.July2021.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get; }
        IRepository<UserAsset> UserAssetRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
