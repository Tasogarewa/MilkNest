using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.Interfaces
{
    public interface IRepository<T> where T:class
    {
        public Task<T> CreateAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(Guid Id);
        public Task<T> GetAsync(Guid Id);
        public Task<IEnumerable<T>> GetAllAsync();
    }
}
