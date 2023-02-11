using System.Net.Http.Json;
using System.Text.Json;
using HttpClientGuide.Client.IServices;
using HttpClientGuide.Shared.Dto;
using HttpClientGuide.Shared.Model;

namespace HttpClientGuide.Client.Services
{
    public class HttpUserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public HttpUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_httpClient.BaseAddress.OriginalString + "api/user/");
            _httpClient.DefaultRequestHeaders.Add("Bearer", "FakeBearerToken");
        }

        public async Task CreateAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("", user);
            response.EnsureSuccessStatusCode();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(await response.Content.ReadAsStreamAsync(), _serializerOptions);
            if (apiResponse is not {Succes: true})
            {
                throw new HttpRequestException(apiResponse?.ResponseMessage);
            }
        }
        public async Task<List<User>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("getall");
            response.EnsureSuccessStatusCode();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<User>>>(await response.Content.ReadAsStreamAsync(), _serializerOptions);
            if (apiResponse is {Succes: true})
            {
                return apiResponse.Data;
            }
            else
            {
                throw new HttpRequestException(apiResponse?.ResponseMessage);
            }
        }

        public async Task UpdateAsync(User user)
        {
            var response = await _httpClient.PutAsJsonAsync("", user);
            response.EnsureSuccessStatusCode();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(await response.Content.ReadAsStreamAsync(), _serializerOptions);
            if (apiResponse is not {Succes: true})
            {
                throw new HttpRequestException(apiResponse?.ResponseMessage);
            }
        }

        public async Task DeleteAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"?id={id}");
            response.EnsureSuccessStatusCode();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(await response.Content.ReadAsStreamAsync(), _serializerOptions);
            if (apiResponse is not {Succes: true})
            {
                throw new HttpRequestException(apiResponse?.ResponseMessage);
            }
        }
    }
}
