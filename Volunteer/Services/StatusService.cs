using Volunteer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volunteer.Services
{
    public class StatusService : IStatusService
    {
        private readonly VolunteerContext _context;

        // -----------------------------------------------------------------

        public StatusService(VolunteerContext context)
        {
            _context = context;
        }

        // -----------------------------------------------------------------

        public async Task<List<Status>> ToListAsync()
        {
            return await _context.Status.ToListAsync();
        }

        // -----------------------------------------------------------------

        public async Task<Status?> GetByIdAsync(int id)
        {
            return await _context.Status.FindAsync(id);
        }

        // -----------------------------------------------------------------

        public async Task AddAsync(Status source)
        {
            _context.Status.Add(source);
            await _context.SaveChangesAsync();
        }

        // -----------------------------------------------------------------

        public async Task UpdateAsync(Status source)
        {
            var existingStatus = await _context.Status.FindAsync(source.StatusId);
            if (existingStatus != null)
            {
                existingStatus.Name = source.Name;
                await _context.SaveChangesAsync();
            }
        }

        // -----------------------------------------------------------------

        public async Task DeleteAsync(int id)
        {
            var source = await _context.Status.FindAsync(id);
            if (source != null)
            {
                _context.Status.Remove(source);
                await _context.SaveChangesAsync();
            }
        }

        // -----------------------------------------------------------------
    }
}
