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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAssetService _assetService;
        private readonly IUserAssetService _userAssetService;
        public UserService(IUnitOfWork unitOfWork, IAssetService assetService, IUserAssetService userAssetService)
        {
            _unitOfWork = unitOfWork;
            _assetService = assetService;
            _userAssetService = userAssetService;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _unitOfWork.UserRepository.GetAll();
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);
            if (user == null) throw new BussinessException("User not Found", 404);

            var assetsList = await _assetService.GetAssets();
            var userAsset = await _userAssetService.GetAssetsByUser(id);
            user.Assets = assetsList.Where(asset => userAsset.Contains(asset.Id)).ToList();
            return user;
        }

        public async Task<User> AddUser(User user)
        {
            var currentUser = await GetUsers();
            if (currentUser.Where(x => x.Email.ToLower() == user.Email.ToLower()).Any())
                throw new BussinessException("An User with same Email Already Exists", 400);

            var assets = await _assetService.GetAssetsFromJsonFile();
            user.AssetName.ForEach(asset =>
            {
                if (!assets.Where(x => x.Name.ToLower() == asset.ToLower() && x.Id.ToLower() == asset.ToLower()).Any())
                    throw new BussinessException("Asset not Found", 404);
            });

            var userCreated = await _unitOfWork.UserRepository.Add(user);
            await _userAssetService.AddUserAsset(user.AssetName, userCreated.Id);
            await _unitOfWork.SaveChangesAsync();

            return userCreated;
        }
        public async Task<bool> UpdateUser(User user)
        {
            var assets = await _assetService.GetAssetsFromJsonFile();
            user.AssetName.ForEach(asset => {
                if (!assets.Where(x => x.Name.ToLower() == asset.ToLower() && x.Id.ToLower() == asset.ToLower()).Any())
                    throw new BussinessException("Asset not Found", 404);
            });

            var currentUser = await GetUser(user.Id);
            await _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            await _userAssetService.UpdateUserAsset(user.AssetName, user.Id);

            return true;
        }
        public async Task<bool> DeleteUser(int id)
        {
            var currentUser = await GetUser(id);
            await _unitOfWork.UserRepository.Delete(currentUser.Id);
            await _userAssetService.DeleteUserAsset(currentUser.Id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
