using Volunteer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volunteer.Services
{
    public class UserSkillService : IUserSkillService
    {
        private readonly VolunteerContext _context;

        // -----------------------------------------------------------------

        public UserSkillService(VolunteerContext context)
        {
            _context = context;
        }

        // -----------------------------------------------------------------

        public async Task<List<UserSkill>> ToListAsync()
        {
            return await _context.UserSkill.ToListAsync();
        }

        // -----------------------------------------------------------------

        public async Task<UserSkill?> GetByIdAsync(int id)
        {
            return await _context.UserSkill.FindAsync(id);
        }

        // -----------------------------------------------------------------

        public async Task AddAsync(UserSkill UserSkill)
        {
            _context.UserSkill.Add(UserSkill);
            await _context.SaveChangesAsync();
        }

        // -----------------------------------------------------------------

        public async Task UpdateAsync(UserSkill UserSkill)
        {
            _context.UserSkill.Update(UserSkill);
            await _context.SaveChangesAsync();
        }

        // -----------------------------------------------------------------

        public async Task DeleteAsync(int id)
        {
            var UserSkill = await _context.UserSkill.FindAsync(id);
            if (UserSkill != null)
            {
                _context.UserSkill.Remove(UserSkill);
                await _context.SaveChangesAsync();
            }
        }

        // -----------------------------------------------------------------
    }
}
