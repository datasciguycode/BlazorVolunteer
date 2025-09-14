using Volunteer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volunteer.Services
{
    public interface IUserInterestService
    {
        Task<List<UserInterest>> ToListAsync();
        Task<UserInterest?> GetByIdAsync(int id);
        Task AddAsync(UserInterest UserInterest);
        Task UpdateAsync(UserInterest UserInterest);
        Task DeleteAsync(int id);
    }
}
