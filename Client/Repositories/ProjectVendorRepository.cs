using System.Net.Http.Headers;
using System.Text;
using API.Dtos.Project;
using API.Dtos.ProjectVendor;
using API.Utilities.Handler;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SupplyManagementSystem.Configurations;

namespace SupplyManagementSystem.Repositories;

public class ProjectVendorRepository
{
     private readonly string _request;
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProjectVendorRepository(IOptions<BaseUrls> connectionConfig, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, string request = "project-vendors/")
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

    public async Task<ResponseDataHandler<GetProjectVendorDto>> Post(CreateProjectVendorDto createProjectVendorDto)
    {
        var content = new StringContent(JsonConvert.SerializeObject(createProjectVendorDto), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_request, content);
        var apiResponse = response.Content.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject<ResponseDataHandler<GetProjectVendorDto>>(apiResponse);
    }
}