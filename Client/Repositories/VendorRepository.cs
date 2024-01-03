using System.Net.Http.Headers;
using System.Text;
using API.Dtos.User;
using API.Dtos.Vendor;
using API.Utilities.Handler;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SupplyManagementSystem.Configurations;

namespace SupplyManagementSystem.Repositories;

public class VendorRepository
{
    private readonly string _request;
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public VendorRepository(IOptions<BaseUrls> connectionConfig, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, string request = "vendors/")
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

    public async Task<List<GetVendorDto>> GetAll()
    {
        ResponseDataHandler<List<GetVendorDto>> entities;

        using var response = await _httpClient.GetAsync(_request);
        var apiResponse = await response.Content.ReadAsStringAsync();
        entities = JsonConvert.DeserializeObject<ResponseDataHandler<List<GetVendorDto>>>(apiResponse);
        
        if (entities.Data != null)
        {
            return entities.Data;
        }

        return (List<GetVendorDto>)Enumerable.Empty<GetVendorDto>();
    }

    public async Task<GetVendorDto?> Get(string guid)
    {
        ResponseDataHandler<GetVendorDto> entities;
        
        using var response = await _httpClient.GetAsync(_request + guid);
        var apiResponse = await response.Content.ReadAsStringAsync();
        entities = JsonConvert.DeserializeObject<ResponseDataHandler<GetVendorDto>>(apiResponse);
        
        return entities.Data ?? null;
    }
    
    public async Task<ResponseHandler> UpdateStatus(UpdateStatusVendorDto updateStatusVendorDto)
    {
        var content = new StringContent(JsonConvert.SerializeObject(updateStatusVendorDto), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(_request + "update-status", content);
        var apiResponse = response.Content.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject<ResponseHandler>(apiResponse);
    }
}