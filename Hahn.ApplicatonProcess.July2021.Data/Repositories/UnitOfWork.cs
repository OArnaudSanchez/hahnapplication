using Hahn.ApplicatonProcess.July2021.Data.Data;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.July2021.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HahnProcessContext _context;
        private IRepository<User> _userRepository;
        private IRepository<UserAsset> _userAssetRepository;

        public UnitOfWork(HahnProcessContext context)
        {
            _context = context;
        }

        public IRepository<User> UserRepository => _userRepository ?? new BaseRepository<User>(_context);
        public IRepository<UserAsset> UserAssetRepository => _userAssetRepository ?? new BaseRepository<UserAsset>(_context);
        public void Dispose()
        {
            if (_context != null) _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
