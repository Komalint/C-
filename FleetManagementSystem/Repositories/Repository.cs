using FleetManagementSystem.Helpers;
using FleetManagementSystem.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetManagementSystem.Repositories
{
    public abstract class Repository<T> : IRepository<T>
    {
        protected readonly SqlHelper<T> _sqlHelper;

        protected Repository(SqlHelper<T> sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }

        public abstract Task CreateAsync(T entity);

        public abstract Task UpdateAsync(T entity);

        public abstract Task DeleteAsync(int id);

        public abstract Task<T> GetByIdAsync(int id);

        public abstract Task<IEnumerable<T>> GetAllAsync();
    }
}