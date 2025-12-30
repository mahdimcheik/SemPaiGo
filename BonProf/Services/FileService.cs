namespace BonProf.Services;

using BonProf.Services.Interfaces;
using SemPaiGo.Utilities;
using System.Net.Http.Headers;

public class SeaweedService : IFileService
{
    private readonly HttpClient _httpClient;
    private readonly string _filerUrl;

    public SeaweedService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _filerUrl = EnvironmentVariables.FilerUrl;
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string folderPath)
    {
        try
        {

        var requestUrl = $"{_filerUrl}/{folderPath.Trim('/')}/{fileName}";

        using var content = new MultipartFormDataContent();
        var streamContent = new StreamContent(fileStream);
        streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        content.Add(streamContent, "file", fileName);

        var response = await _httpClient.PostAsync(requestUrl, content);
        response.EnsureSuccessStatusCode();

        return requestUrl; // Retourne le chemin d'accès au fichier
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de l'upload du fichier: {ex.Message}", ex);
        }
    }

    public async Task<(Stream Content, string ContentType)> DownloadFileAsync(string filePath)
    {
        var requestUrl = $"{_filerUrl}/{filePath.TrimStart('/')}";
        var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);

        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync();
        var contentType = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream";

        return (stream, contentType);
    }

    // SUPPRESSION
    public async Task<bool> DeleteFileAsync(string filePath)
    {
        var requestUrl = $"{_filerUrl}/{filePath.TrimStart('/')}";
        var response = await _httpClient.DeleteAsync(requestUrl);

        return response.IsSuccessStatusCode;
    }
}

public class FileService : IFileService
{
    private readonly HttpClient _httpClient;
    private readonly string _filerUrl;

    public FileService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _filerUrl = EnvironmentVariables.FilerUrl;
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string folderPath)
    {
        try
        {

            var requestUrl = $"{_filerUrl}/{folderPath.Trim('/')}/{fileName}";

            using var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            content.Add(streamContent, "file", fileName);

            var response = await _httpClient.PostAsync(requestUrl, content);
            response.EnsureSuccessStatusCode();

            return requestUrl; // Retourne le chemin d'accès au fichier
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de l'upload du fichier: {ex.Message}", ex);
        }
    }

    public async Task<(Stream Content, string ContentType)> DownloadFileAsync(string filePath)
    {
        var requestUrl = $"{_filerUrl}/{filePath.TrimStart('/')}";
        var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);

        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync();
        var contentType = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream";

        return (stream, contentType);
    }

    // SUPPRESSION
    public async Task<bool> DeleteFileAsync(string filePath)
    {
        var requestUrl = $"{_filerUrl}/{filePath.TrimStart('/')}";
        var response = await _httpClient.DeleteAsync(requestUrl);

        return response.IsSuccessStatusCode;
    }
}

