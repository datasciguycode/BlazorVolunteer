using Volunteer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volunteer.Services
{
    public class RoleService : IRoleService
    {
        private readonly VolunteerContext _context;

        // -----------------------------------------------------------------

        public RoleService(VolunteerContext context)
        {
            _context = context;
        }

        // -----------------------------------------------------------------

        public async Task<List<Role>> ToListAsync()
        {
            return await _context.Role.ToListAsync();
        }

        // -----------------------------------------------------------------

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _context.Role.FindAsync(id);
        }

        // -----------------------------------------------------------------

        public async Task AddAsync(Role Role)
        {
            _context.Role.Add(Role);
            await _context.SaveChangesAsync();
        }

        // -----------------------------------------------------------------

        public async Task UpdateAsync(Role role)
        {
            var existingRole = await _context.Role.FindAsync(role.RoleId);
            if (existingRole == null)
            {
                throw new KeyNotFoundException($"Role with ID {role.RoleId} not found.");
            }
        
            // Update the properties of the tracked entity
            existingRole.Name = role.Name;
            existingRole.Password = role.Password;
            
            // Save changes
            await _context.SaveChangesAsync();
        }

        // -----------------------------------------------------------------

        public async Task DeleteAsync(int id)
        {
            var Role = await _context.Role.FindAsync(id);
            if (Role != null)
            {
                _context.Role.Remove(Role);
                await _context.SaveChangesAsync();
            }
        }

        // -----------------------------------------------------------------
    }
}
