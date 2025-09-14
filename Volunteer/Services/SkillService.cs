using Volunteer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volunteer.Services
{
    public class SkillService : ISkillService
    {
        private readonly VolunteerContext _context;

        // -----------------------------------------------------------------

        public SkillService(VolunteerContext context)
        {
            _context = context;
        }

        // -----------------------------------------------------------------

        public async Task<List<Skill>> ToListAsync()
        {
            return await _context.Skill.ToListAsync();
        }

        // -----------------------------------------------------------------

        public async Task<Skill?> GetByIdAsync(int id)
        {
            return await _context.Skill.FindAsync(id);
        }

        // -----------------------------------------------------------------

        public async Task AddAsync(Skill interest)
        {
            _context.Skill.Add(interest);
            await _context.SaveChangesAsync();
        }

        // -----------------------------------------------------------------

        public async Task UpdateAsync(Skill interest)
        {
            var existingSkill = await _context.Skill.FindAsync(interest.SkillId);
            if (existingSkill != null)
            {
                existingSkill.Name = interest.Name;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Skill with ID {interest.SkillId} not found.");
            }
        }

        // -----------------------------------------------------------------

        public async Task DeleteAsync(int id)
        {
            var interest = await _context.Skill.FindAsync(id);
            if (interest != null)
            {
                _context.Skill.Remove(interest);
                await _context.SaveChangesAsync();
            }
        }

        // -----------------------------------------------------------------
    }
}
