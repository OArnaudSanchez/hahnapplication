using Hahn.ApplicatonProcess.July2021.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.Interfaces
{
    public interface IUserAssetService
    {
        Task<List<UserAsset>> GetUserAssets();
        Task<UserAsset> GetUserAsset(int id);
        Task<List<string>> GetAssetsByUser(int id);
        Task AddUserAsset(List<string> assets, int id);
        Task<bool> UpdateUserAsset(List<string> assets, int id);
        Task<bool> DeleteUserAsset(int id);
    }
}
