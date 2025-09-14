namespace Volunteer.Services;

public interface IAuthService
{
    bool IsAuthenticated { get; }
    bool IsAdmin { get; }
    bool IsBasicUser { get; }
    bool IsPowerUser { get; }
    bool CanEdit { get; }
    bool CanAdd { get; }
    event Action? AuthenticationStateChanged;
    Task<bool> ValidatePasswordAsync(string password);
    void Logout();
}