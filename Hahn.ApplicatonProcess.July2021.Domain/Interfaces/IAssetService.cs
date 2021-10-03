using Hahn.ApplicatonProcess.July2021.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.Interfaces
{
    public interface IAssetService
    {
        Task<List<Asset>> GetAssets();
        Task<List<Asset>> GetAssetsFromJsonFile();
        Task<Asset> GetAsset(string id);
    }
}
