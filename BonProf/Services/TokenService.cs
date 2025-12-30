using BonProf.Models.UtilityModels;
using BonProf.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using SemPaiGo.Utilities;
using System.IO;
using System.Web;

namespace BonProf.Services;

public class TokenService : ITokenService
{
    public static string FilerToken = "";
    private readonly HttpClient _httpClient;
    private readonly string _filerUrl;

    public TokenService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _filerUrl = EnvironmentVariables.FilerUrl ?? throw new ArgumentNullException("FilerUrl is missing");
    }

    public async Task<string> GetAsync(string serviceName)
    {
        var uri = new UriBuilder($"{_filerUrl}/auth");
        var query = HttpUtility.ParseQueryString(uri.Query);
        query["serviceName"] = serviceName;
        uri.Query = query.ToString();

        using var response = await _httpClient.GetAsync(uri.Uri);
        response.EnsureSuccessStatusCode();

        var bodyAsText = await response.Content.ReadAsStringAsync();
        var bodyAsClass = System.Text.Json.JsonSerializer.Deserialize<FilerAuthResponse>(bodyAsText) ?? throw new Exception("Token deserialization failed");

        return bodyAsClass.Token;
    }

    public async Task RefreshAsync(string serviceName)
    {
    }
}
