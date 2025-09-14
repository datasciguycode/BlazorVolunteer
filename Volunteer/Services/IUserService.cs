using Volunteer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volunteer.Services
{
    public interface IUserService
    {
        Task<List<User>> ToListAsync();
        Task<List<User>> GetTopUsersAsync(int recordCount);
        Task<int> GetTotalUserCountAsync();
        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
    }
}
