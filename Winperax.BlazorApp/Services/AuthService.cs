using System.Net.Http.Json;

public class AuthService
{
    private readonly HttpClient _http;

    public AuthService(HttpClient http)
    {
        _http = http;
    }

    public async Task<bool> LoginAsync(LoginModel login)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", login);
        return response.IsSuccessStatusCode;
    }

    public class LoginModel
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
