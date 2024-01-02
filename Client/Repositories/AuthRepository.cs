using System.Text;
using API.Dtos.Login;
using API.Entities;
using API.Repositories;
using API.Utilities.Handler;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SupplyManagementSystem.Configurations;

namespace SupplyManagementSystem.Repositories;

public class AuthRepository
{
    private readonly BaseUrls _baseUrls;
    private readonly string _request;
    private readonly HttpClient httpClient;
    public AuthRepository(IOptions<BaseUrls> connectionConfig, string request = "auth/")
    {
        _request = request;
        _baseUrls = connectionConfig.Value;
        httpClient = new HttpClient
        {
            BaseAddress = new Uri(_baseUrls.BaseUrlApis)
        };
    }
    
    public async Task<ResponseDataHandler<TokenDto>> Auth(LoginDto loginDto)
    {
        var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(_request + "login", content);
        var apiResponse = response.Content.ReadAsStringAsync().Result;
        
        var jwtToken = JsonConvert.DeserializeObject<ResponseDataHandler<TokenDto>>(apiResponse);
        
        return jwtToken;

    }
}