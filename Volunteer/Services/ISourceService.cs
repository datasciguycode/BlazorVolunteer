using Volunteer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volunteer.Services
{
    public interface ISourceService
    {
        Task<List<Source>> ToListAsync();
        Task<Source?> GetByIdAsync(int id);
        Task AddAsync(Source source);
        Task UpdateAsync(Source source);
        Task DeleteAsync(int id);
    }
}
