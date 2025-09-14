using Volunteer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volunteer.Services
{
    public class UserInterestService : IUserInterestService
    {
        private readonly VolunteerContext _context;

        // -----------------------------------------------------------------

        public UserInterestService(VolunteerContext context)
        {
            _context = context;
        }

        // -----------------------------------------------------------------

        public async Task<List<UserInterest>> ToListAsync()
        {
            return await _context.UserInterest.ToListAsync();
        }

        // -----------------------------------------------------------------

        public async Task<UserInterest?> GetByIdAsync(int id)
        {
            return await _context.UserInterest.FindAsync(id);
        }

        // -----------------------------------------------------------------

        public async Task AddAsync(UserInterest UserInterest)
        {
            _context.UserInterest.Add(UserInterest);
            await _context.SaveChangesAsync();
        }

        // -----------------------------------------------------------------

        public async Task UpdateAsync(UserInterest UserInterest)
        {
            _context.UserInterest.Update(UserInterest);
            await _context.SaveChangesAsync();
        }

        // -----------------------------------------------------------------

        public async Task DeleteAsync(int id)
        {
            var UserInterest = await _context.UserInterest.FindAsync(id);
            if (UserInterest != null)
            {
                _context.UserInterest.Remove(UserInterest);
                await _context.SaveChangesAsync();
            }
        }

        // -----------------------------------------------------------------
    }
}
