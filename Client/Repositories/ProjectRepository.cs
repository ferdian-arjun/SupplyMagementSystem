using System.Net.Http.Headers;
using System.Text;
using API.Dtos.Project;
using API.Dtos.Project;
using API.Utilities.Handler;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SupplyManagementSystem.Configurations;

namespace SupplyManagementSystem.Repositories;

public class ProjectRepository
{
    private readonly string _request;
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProjectRepository(IOptions<BaseUrls> connectionConfig, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, string request = "projects/")
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

    public async Task<List<GetProjectDto>> GetAll()
    {
        ResponseDataHandler<List<GetProjectDto>> entities;

        using var response = await _httpClient.GetAsync(_request);
        var apiResponse = await response.Content.ReadAsStringAsync();
        entities = JsonConvert.DeserializeObject<ResponseDataHandler<List<GetProjectDto>>>(apiResponse);
        
        if (entities.Data != null)
        {
            return entities.Data;
        }

        return (List<GetProjectDto>)Enumerable.Empty<GetProjectDto>();
    }

    public async Task<ResponseDataHandler<GetProjectDto>> Post(CreateProjectDto createProjectDto)
    {
        var content = new StringContent(JsonConvert.SerializeObject(createProjectDto), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_request, content);
        var apiResponse = response.Content.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject<ResponseDataHandler<GetProjectDto>>(apiResponse);
    }

    public async Task<GetProjectDto?> Get(string guid)
    {
        ResponseDataHandler<GetProjectDto> entities;
        
        using var response = await _httpClient.GetAsync(_request + guid);
        var apiResponse = await response.Content.ReadAsStringAsync();
        entities = JsonConvert.DeserializeObject<ResponseDataHandler<GetProjectDto>>(apiResponse);
        
        return entities.Data ?? null;
    }

    public async Task<ResponseHandler> Put(UpdateProjectDto updateProjectDto)
    {
        var content = new StringContent(JsonConvert.SerializeObject(updateProjectDto), Encoding.UTF8, "application/json");
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