namespace BonProf.Services.Interfaces;

public interface ITokenService
{
    Task<string> GetAsync(string serviceName);
    Task RefreshAsync(string serviceName);
}
