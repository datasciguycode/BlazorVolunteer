using System.Collections.Generic;

namespace Volunteer.Services
{
    // Holds session-specific state for the current user
    public class UserSessionState
    {
        // Tracks user IDs created during this session (for Basic users)
        public HashSet<int> SessionCreatedUserIds { get; } = new();

        public void Clear()
        {
            SessionCreatedUserIds.Clear();
        }
    }
}