using Volunteer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volunteer.Services
{
    public interface IStatusService
    {
        Task<List<Status>> ToListAsync();
        Task<Status?> GetByIdAsync(int id);
        Task AddAsync(Status status);
        Task UpdateAsync(Status status);
        Task DeleteAsync(int id);
    }
}
