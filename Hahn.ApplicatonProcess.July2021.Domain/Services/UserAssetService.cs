using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.July2021.Domain.Models;
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
        public UserAssetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<UserAsset>> GetUserAssets()
        {
            var result = await _unitOfWork.UserAssetRepository.GetAll();
            return result;
        }

        public async Task<UserAsset> GetUserAsset(int id)
        {
            return await _unitOfWork.UserAssetRepository.GetById(id);
        }

        public async Task<List<string>> GetAssetsByUser(int id)
        {
            var assets = await _unitOfWork.UserAssetRepository.GetAll();
            return assets.Where(x => x.IdUser == id).Select(x => x.IdAsset).ToList();
        }

        public async Task AddUserAsset(List<string> assets, int idUser)
        {
            assets.ForEach(async asset => await _unitOfWork.UserAssetRepository.Add(new UserAsset { IdAsset = asset.ToString(), IdUser = idUser }));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateUserAsset(List<string> assets, int idUser)
        {
            var currentAsset = await GetAssetsByUser(idUser);
            currentAsset = assets;
            currentAsset.ForEach(asset => _unitOfWork.UserAssetRepository.Update(new UserAsset { IdAsset = asset.ToString(), IdUser = idUser }));
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsset(int id)
        {
            var userAsset = await GetUserAsset(id);
            await _unitOfWork.UserAssetRepository.Delete(userAsset.Id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
