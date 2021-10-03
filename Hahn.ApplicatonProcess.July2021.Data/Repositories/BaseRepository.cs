using Hahn.ApplicatonProcess.July2021.Data.Data;
using Hahn.ApplicatonProcess.July2021.Domain.Common;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Data.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly HahnProcessContext _context;
        protected readonly DbSet<T> _entities;

        public BaseRepository(HahnProcessContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }
        public async Task<List<T>> GetAll()
        {
            return await _entities.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.AsNoTracking().FirstOrDefaultAsync( x => x.Id == id );
        }
        public async Task<T> Add(T Entity)
        {
            await _entities.AddAsync(Entity);
            return Entity;
        }

        public async Task<bool> Update(T Entity)
        {
            var currentEntity = await GetById(Entity.Id);
            _entities.Update(Entity);
            return true;
        }
        public async Task<bool> Delete(int id)
        {
            var currentEntity = await GetById(id);
            _entities.Remove(currentEntity);
            return true;
        }
    }
}
