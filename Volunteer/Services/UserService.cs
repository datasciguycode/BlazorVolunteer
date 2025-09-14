using Volunteer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volunteer.Services
{
    public class UserService : IUserService
    {
        private readonly VolunteerContext _context;
        private readonly IAuthService _authService;

        private const int BASIC_USER_RECORD_LIMIT = 5;

        // -----------------------------------------------------------------

        public UserService(VolunteerContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        // -----------------------------------------------------------------

        public async Task<List<User>> ToListAsync()
        {
            return await _context.User.ToListAsync();
        }

        // -----------------------------------------------------------------

        public async Task<List<User>> GetTopUsersAsync(int requestedCount)
        {
            // If the user has the Basic role, limit to 5 records regardless of requested count
            int actualLimit = _authService.IsBasicUser ? BASIC_USER_RECORD_LIMIT : requestedCount;

            return await _context.User
                .OrderByDescending(u => u.UserId) // Most recently modified first
                .Take(actualLimit)
                .ToListAsync();
        }

        // -----------------------------------------------------------------

        public async Task<int> GetTotalUserCountAsync()
        {
            return await _context.User.CountAsync();
        }

        // -----------------------------------------------------------------

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.User.FindAsync(id);
        }

        // -----------------------------------------------------------------

        public async Task AddAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }

        // -----------------------------------------------------------------

        public async Task UpdateAsync(User user)
        {
            _context.User.Update(user);
            await _context.SaveChangesAsync();
        }

        // -----------------------------------------------------------------

        public async Task DeleteAsync(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        // -----------------------------------------------------------------
    }
}
