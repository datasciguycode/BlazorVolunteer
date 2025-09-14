using Volunteer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volunteer.Services
{
    public class SourceService : ISourceService
    {
        private readonly VolunteerContext _context;

        // -----------------------------------------------------------------

        public SourceService(VolunteerContext context)
        {
            _context = context;
        }

        // -----------------------------------------------------------------

        public async Task<List<Source>> ToListAsync()
        {
            return await _context.Source.ToListAsync();
        }

        // -----------------------------------------------------------------

        public async Task<Source?> GetByIdAsync(int id)
        {
            return await _context.Source.FindAsync(id);
        }

        // -----------------------------------------------------------------

        public async Task AddAsync(Source source)
        {
            _context.Source.Add(source);
            await _context.SaveChangesAsync();
        }

        // -----------------------------------------------------------------

        public async Task UpdateAsync(Source source)
        {
            var existingSource = await _context.Source.FindAsync(source.SourceId);
            if (existingSource != null)
            {
                existingSource.Name = source.Name;
                await _context.SaveChangesAsync();
            }
        }

        // -----------------------------------------------------------------

        public async Task DeleteAsync(int id)
        {
            var source = await _context.Source.FindAsync(id);
            if (source != null)
            {
                _context.Source.Remove(source);
                await _context.SaveChangesAsync();
            }
        }

        // -----------------------------------------------------------------
    }
}
