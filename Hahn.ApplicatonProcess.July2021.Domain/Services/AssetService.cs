using Hahn.ApplicatonProcess.July2021.Domain.Exceptions;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.July2021.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.Services
{
    public class AssetService : IAssetService
    {
        private readonly IDataAcess _dataAccess;
        public AssetService(IDataAcess dataAcess)
        {
            _dataAccess = dataAcess;
        }
        public async Task<List<Asset>> GetAssets()
        {
            return await GetAssetsFromJsonFile();
        }


        public async Task<List<Asset>> GetAssetsFromJsonFile()
        {
            await _dataAccess.FetchAssets();
            var assets = await _dataAccess.GetDataFromJsonFile();
            return assets;
        }

        public async Task<Asset> GetAsset(string id)
        {
            var assets = await GetAssetsFromJsonFile();
            return assets.FirstOrDefault(x => x.Id == id) ?? throw new BussinessException("Asset Not Found", 404);
        }
    }
}
