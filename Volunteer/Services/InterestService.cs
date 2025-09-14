using Volunteer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volunteer.Services
{
    public class InterestService : IInterestService
    {
        private readonly VolunteerContext _context;

        // -----------------------------------------------------------------

        public InterestService(VolunteerContext context)
        {
            _context = context;
        }

        // -----------------------------------------------------------------

        public async Task<List<Interest>> ToListAsync()
        {
            return await _context.Interest.ToListAsync();
        }

        // -----------------------------------------------------------------

        public async Task<Interest?> GetByIdAsync(int id)
        {
            return await _context.Interest.FindAsync(id);
        }

        // -----------------------------------------------------------------

        public async Task AddAsync(Interest interest)
        {
            _context.Interest.Add(interest);
            await _context.SaveChangesAsync();
        }

        // -----------------------------------------------------------------

        public async Task UpdateAsync(Interest interest)
        {
            var existingInterest = await _context.Interest.FindAsync(interest.InterestId);
            if (existingInterest != null)
            {
                existingInterest.Name = interest.Name;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Interest with ID {interest.InterestId} not found.");
            }
        }

        // -----------------------------------------------------------------

        public async Task DeleteAsync(int id)
        {
            var interest = await _context.Interest.FindAsync(id);
            if (interest != null)
            {
                _context.Interest.Remove(interest);
                await _context.SaveChangesAsync();
            }
        }

        // -----------------------------------------------------------------
    }
}
