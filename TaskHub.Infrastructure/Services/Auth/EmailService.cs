using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TaskHub.Application.Interfaces;

namespace TaskHub.Infrastructure.Services.Auth
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public EmailService (HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> SendEmailAsync(string to, string subject, string body, CancellationToken ct)
        {
            var accessToken = await GetAccessTokenAsync(ct);

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://gmail.googleapis.com/gmail/v1/users/me/messages/send"
            );

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var emailText =
            $"""
            To: {to}
            Subject: {subject}
            MIME-Version: 1.0
            Content-Type: text/plain; charset=UTF-8

            {body}
            """;

            var base64Url = Convert.ToBase64String(Encoding.UTF8.GetBytes(emailText))
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');

            var json = JsonSerializer.Serialize(new
            {
                raw = base64Url
            });

            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request, ct);
            var responseBody = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Gmail API error: {(int)response.StatusCode} - {responseBody}");
            }

            return responseBody;
        }

        private async Task<string> GetAccessTokenAsync(CancellationToken ct)
        {
            using var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://oauth2.googleapis.com/token"
            );

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["client_id"] = _configuration["Gmail:client_id"]!,
                ["client_secret"] = _configuration["Gmail:client_secret"]!,
                ["refresh_token"] = _configuration["Gmail:refresh_token"]!,
                ["grant_type"] = "refresh_token"
            });

            var response = await _httpClient.SendAsync(request, ct);

            var json = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
                throw new Exception(json);

            using var obj = JsonDocument.Parse(json);

            return obj.RootElement.GetProperty("access_token").GetString()!;
        }
    }
}
