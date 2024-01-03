using System.Net.Http.Headers;
using System.Text;
using API.Dtos.Company;
using API.Utilities.Handler;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SupplyManagementSystem.Configurations;

namespace SupplyManagementSystem.Repositories;

public class CompanyRepository
{
     private readonly string _request;
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CompanyRepository(IOptions<BaseUrls> connectionConfig, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, string request = "companies/")
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

        public async Task<IEnumerable<GetCompanyWithStatusDto>> GetAll()
        {
            ResponseDataHandler<List<GetCompanyWithStatusDto>> entities;

            using var response = await _httpClient.GetAsync(_request);
            var apiResponse = await response.Content.ReadAsStringAsync();
            entities = JsonConvert.DeserializeObject<ResponseDataHandler<List<GetCompanyWithStatusDto>>>(apiResponse);
            
            return entities.Data ?? Enumerable.Empty<GetCompanyWithStatusDto>();
        }

        public async Task<ResponseDataHandler<GetCompanyDto>> Post(CreateCompanyDto createCompanyDto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(createCompanyDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_request, content);
            var apiResponse = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<ResponseDataHandler<GetCompanyDto>>(apiResponse);
        }

        public async Task<GetCompanyDto?> Get(string guid)
        {
            ResponseDataHandler<GetCompanyDto> entities;
            
            using var response = await _httpClient.GetAsync(_request + guid);
            var apiResponse = await response.Content.ReadAsStringAsync();
            entities = JsonConvert.DeserializeObject<ResponseDataHandler<GetCompanyDto>>(apiResponse);
            
            return entities.Data ?? null;
        }
        
        public async Task<ResponseHandler> Put(UpdateCompanyDto updateCompanyDto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(updateCompanyDto), Encoding.UTF8, "application/json");
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

        public async Task<IEnumerable<GetWaitingForApprovalDto>> GetWaitingForApproval()
        {
            ResponseDataHandler<List<GetWaitingForApprovalDto>> entities;

            using var response = await _httpClient.GetAsync(_request + "get-waiting-for-approval");
            var apiResponse = await response.Content.ReadAsStringAsync();
            entities = JsonConvert.DeserializeObject<ResponseDataHandler<List<GetWaitingForApprovalDto>>>(apiResponse);
            
            return entities.Data ?? Enumerable.Empty<GetWaitingForApprovalDto>();
        }
}