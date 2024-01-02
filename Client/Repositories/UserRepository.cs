using System.Net.Http.Headers;
using System.Text;
using API.Dtos.User;
using API.Entities;
using API.Repositories;
using API.Utilities.Handler;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SupplyManagementSystem.Configurations;

namespace SupplyManagementSystem.Repositories
{
    public class UserRepository
    {
        private readonly string _request;
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(IOptions<BaseUrls> connectionConfig, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, string request = "users/")
        {
            _request = request;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;

            var baseUrls = connectionConfig.Value;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrls.BaseUrlApis)
            };

            // Retrieve access token from the session
            var accessToken = _httpContextAccessor.HttpContext?.Session.GetString("JWToken");
            
            
            if (!string.IsNullOrEmpty(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
        }

        public async Task<List<GetUserDto>> GetAll()
        {
            ResponseDataHandler<List<GetUserDto>> entities;

            using var response = await _httpClient.GetAsync(_request);
            var apiResponse = await response.Content.ReadAsStringAsync();
            entities = JsonConvert.DeserializeObject<ResponseDataHandler<List<GetUserDto>>>(apiResponse);
            
            if (entities.Data != null)
            {
                return entities.Data;
            }

            return (List<GetUserDto>)Enumerable.Empty<GetUserDto>();
        }

        public async Task<ResponseDataHandler<GetUserDto>> Post(CreateUserDto createUserDto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(createUserDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_request, content);
            var apiResponse = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<ResponseDataHandler<GetUserDto>>(apiResponse);
        }

        public async Task<GetUserDto?> Get(string guid)
        {
            ResponseDataHandler<GetUserDto> entities;
            
            using var response = await _httpClient.GetAsync(_request + guid);
            var apiResponse = await response.Content.ReadAsStringAsync();
            entities = JsonConvert.DeserializeObject<ResponseDataHandler<GetUserDto>>(apiResponse);
            
            return entities.Data ?? null;
        }
        
        public async Task<ResponseHandler> Put(UpdateUserDto updateUserDto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(updateUserDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_request, content);
            var apiResponse = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<ResponseHandler>(apiResponse);
        }
        
        public async Task<ResponseHandler> Delete(string guid)
        {
            var response = await _httpClient.DeleteAsync(_request + guid);
            var apiResponse = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<ResponseHandler>(apiResponse);
        }
    }
}
