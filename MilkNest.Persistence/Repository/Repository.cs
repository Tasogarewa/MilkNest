using Microsoft.EntityFrameworkCore;
using MilkNest.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MilkNestDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public Repository(MilkNestDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(entity);
        }

        public async Task DeleteAsync(Guid Id)
        {
            T? entity = await _dbSet.FindAsync(Id);
            if (entity != null)
            {
                
                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetAsync(Guid Id)
        {
            T? entity = await _dbSet.FindAsync(Id);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
