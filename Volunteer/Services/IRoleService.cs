using Volunteer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volunteer.Services
{
    public interface IRoleService
    {
        Task<List<Role>> ToListAsync();
        Task<Role?> GetByIdAsync(int id);
        Task AddAsync(Role role);
        Task UpdateAsync(Role role);
        Task DeleteAsync(int id);
    }
}
