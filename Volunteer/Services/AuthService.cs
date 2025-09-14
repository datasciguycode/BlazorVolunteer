using Volunteer.Services;

namespace Volunteer.Services;

public class AuthService : IAuthService
{
    private readonly IRoleService _roleService;
    private bool _isAuthenticated;
    private bool _isAdmin;
    private bool _isBasicUser;
    private bool _isPowerUser;

    public bool IsAuthenticated => _isAuthenticated;
    public bool IsAdmin => _isAdmin;
    public bool IsBasicUser => _isBasicUser;
    public bool IsPowerUser => _isPowerUser;
    public bool CanEdit => _isAdmin || _isPowerUser; // Admin and Power can edit
    public bool CanAdd => _isAuthenticated; // Admin, Power, and Basic can add
    public event Action? AuthenticationStateChanged;

    // -----------------------------------------------------------------

    public AuthService(IRoleService roleService)
    {
        _roleService = roleService;
    }

    // -----------------------------------------------------------------

    public async Task<bool> ValidatePasswordAsync(string password)
    {
        var wasAuthenticated = _isAuthenticated;
        var wasAdmin = _isAdmin;
        var wasBasicUser = _isBasicUser;
        var wasPowerUser = _isPowerUser;

        // Reset authentication state
        _isAuthenticated = false;
        _isAdmin = false;
        _isBasicUser = false;
        _isPowerUser = false;

        try
        {
            // Get all roles from the database
            var roles = await _roleService.ToListAsync();

            // Check if password matches any role
            foreach (var role in roles)
            {
                if (role.Password == password)
                {
                    _isAuthenticated = true;

                    // Determine role type based on role name
                    if (role.Name.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        _isAdmin = true;
                        _isBasicUser = false;
                        _isPowerUser = false;
                    }
                    else if (role.Name.Equals("Basic", StringComparison.OrdinalIgnoreCase))
                    {
                        _isAdmin = false;
                        _isBasicUser = true;
                        _isPowerUser = false;
                    }
                    else if (role.Name.Equals("Power", StringComparison.OrdinalIgnoreCase))
                    {
                        _isAdmin = false;
                        _isBasicUser = false;
                        _isPowerUser = true;
                    }

                    break;
                }
            }
        }
        catch (Exception)
        {
            // Log exception if needed
            _isAuthenticated = false;
            _isAdmin = false;
            _isBasicUser = false;
            _isPowerUser = false;
        }

        // Notify if authentication state changed
        if (wasAuthenticated != _isAuthenticated || wasAdmin != _isAdmin || wasBasicUser != _isBasicUser || wasPowerUser != _isPowerUser)
        {
            AuthenticationStateChanged?.Invoke();
        }

        return _isAuthenticated;
    }

    // -----------------------------------------------------------------

    public void Logout()
    {
        var wasAuthenticated = _isAuthenticated;
        _isAuthenticated = false;
        _isAdmin = false;
        _isBasicUser = false;
        _isPowerUser = false;

        if (wasAuthenticated)
        {
            AuthenticationStateChanged?.Invoke();
        }
    }

    // -----------------------------------------------------------------
}