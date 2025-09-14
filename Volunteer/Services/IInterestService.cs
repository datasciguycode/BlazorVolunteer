using Volunteer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volunteer.Services
{
    public interface IInterestService
    {
        Task<List<Interest>> ToListAsync();
        Task<Interest?> GetByIdAsync(int id);
        Task AddAsync(Interest interest);
        Task UpdateAsync(Interest interest);
        Task DeleteAsync(int id);
    }
}
