using Volunteer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volunteer.Services
{
    public interface IUserSkillService
    {
        Task<List<UserSkill>> ToListAsync();
        Task<UserSkill?> GetByIdAsync(int id);
        Task AddAsync(UserSkill UserSkill);
        Task UpdateAsync(UserSkill UserSkill);
        Task DeleteAsync(int id);
    }
}
