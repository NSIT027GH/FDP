using FDP.WebServer.Components.Models;
using Microsoft.AspNetCore.Identity.Data;
using System.Text;
using System.Text.Json;

namespace FDP.WebServer.Components.Service;

public class UserService
{
    private readonly HttpClient _httpClient;
    private readonly string _httpURL = "https://localhost:44320/api/User"; // Replace with your actual API URL
    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<UserDetails>> GetUserAsync()
    {
        var response = await _httpClient.GetAsync(_httpURL + "/GetUserDetails");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"API Error: {response.StatusCode}");
        }

        var json = await response.Content.ReadAsStringAsync();

        var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<UserDetails>>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return apiResponse?.Data ?? new List<UserDetails>();
    }

    public async Task<ApiResponse<object>> AddUserData(AddUserRequest request)
    {
        //var command = new { Id = id }; // Or use DeleteUserCommand class if shared
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_httpURL}/UserRegistration", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"API returned error: {response.StatusCode}, {error}");
        }

        var responseBody = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ApiResponse<object>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<ApiResponse<object>> UpdateUserData(UpdateUserRequest request)
    {
        //var command = new { Id = id }; // Or use DeleteUserCommand class if shared
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{_httpURL}/UpdateUserDetails", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"API returned error: {response.StatusCode}, {error}");
        }

        var responseBody = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ApiResponse<object>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<ApiResponse<object>> DeleteUserData(int id)
    {
        var command = new { Id = id }; // Or use DeleteUserCommand class if shared
        var json = JsonSerializer.Serialize(command);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{_httpURL}/DeleteUserDetails", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"API returned error: {response.StatusCode}, {error}");
        }

        var responseBody = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ApiResponse<object>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
    public async Task<ApiResponse<object>> UserLogin(LoginRequest request)
    {
        //var command = new { Id = id }; // Or use DeleteUserCommand class if shared
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{_httpURL}/UserLogin", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"API returned error: {response.StatusCode}, {error}");
        }

        var responseBody = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ApiResponse<object>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    }
}
