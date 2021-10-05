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
    public class UserAssetService : IUserAssetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        public UserAssetService(IUnitOfWork unitOfWork, ILogger<UserAssetService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<List<UserAsset>> GetUserAssets()
        {
            _logger.LogInformation("The Method GetUserAssets in the UserAssetService has been accessed");
            var result = await _unitOfWork.UserAssetRepository.GetAll();
            return result;
        }

        public async Task<UserAsset> GetUserAsset(int id)
        {
            _logger.LogInformation("The Method GetUserAsset by id in the UserAssetService has been accessed");
            return await _unitOfWork.UserAssetRepository.GetById(id);
        }

        public async Task<List<string>> GetAssetsByUser(int id)
        {
            _logger.LogInformation("The Method GetAssetsByUser in the UserAssetService has been accessed");
            var assets = await _unitOfWork.UserAssetRepository.GetAll();
            return assets.Where(x => x.IdUser == id).Select(x => x.IdAsset).ToList();
        }

        public async Task AddUserAsset(List<string> assets, int idUser)
        {
            _logger.LogInformation("The Method AddUserAsset in the UserAssetService has been accessed");
            assets.ForEach(async asset => await _unitOfWork.UserAssetRepository.Add(new UserAsset { IdAsset = asset.ToString(), IdUser = idUser }));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateUserAsset(List<string> assets, int idUser)
        {
            _logger.LogInformation("The Method UpdateUserAsset in the UserAssetService has been accessed");
            var currentAsset = await GetAssetsByUser(idUser);
            currentAsset = assets;
            currentAsset.ForEach(asset => _unitOfWork.UserAssetRepository.Update(new UserAsset { IdAsset = asset.ToString(), IdUser = idUser }));
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsset(int id)
        {
            _logger.LogInformation("The Method DeleteUserAsset in the UserAssetService has been accessed");
            var userAsset = await GetUserAsset(id);
            await _unitOfWork.UserAssetRepository.Delete(userAsset.Id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
