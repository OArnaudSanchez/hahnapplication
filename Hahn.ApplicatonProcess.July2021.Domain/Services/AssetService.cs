using Hahn.ApplicatonProcess.July2021.Domain.Exceptions;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.July2021.Domain.Models;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;
        public AssetService(IDataAcess dataAcess, ILogger<AssetService> logger)
        {
            _dataAccess = dataAcess;
            _logger = logger;
            
        }
        public async Task<List<Asset>> GetAssets()
        {
            _logger.LogInformation("The Method GetAssets in the AssetService has been accessed");
            return await GetAssetsFromJsonFile();
        }


        public async Task<List<Asset>> GetAssetsFromJsonFile()
        {
            _logger.LogInformation("The Method GetAssetsFromJsonFile in the AssetService has been accessed");
            await _dataAccess.FetchAssets();
            var assets = await _dataAccess.GetDataFromJsonFile();
            return assets;
        }

        public async Task<Asset> GetAsset(string id)
        {
            _logger.LogInformation("The Method GetAsset By id in the AssetService has been accessed");
            var assets = await GetAssetsFromJsonFile();
            return assets.FirstOrDefault(x => x.Id == id) ?? throw new BussinessException("Asset Not Found", 404);
        }
    }
}
