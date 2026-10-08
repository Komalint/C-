using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetManagementSystem.Interfaces
{
    public interface IRepository<T>
    {
        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);

        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();
    }
}