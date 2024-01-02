using SupplyManagementSystem.Controllers;
using SupplyManagementSystem.Repositories;

namespace SupplyManagementSystem.Configurations;

public class ServiceConfiguration
{
    private IConfiguration? _configuration;
    private IServiceCollection? _services;

    public ServiceConfiguration Service(IServiceCollection services)
    {
        
        services.AddDistributedMemoryCache();
        services.AddMvc();
        services.AddMemoryCache();
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(10);//You can set Time   
        });
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        // services.AddSingleton(_configuration.GetSection("ConnectionStrings").Get<ConnectionConfig>());
        // services.Configure<ConnectionConfig>(_configuration.GetSection("ConnectionStrings"));
        services.AddControllersWithViews();
        services.AddScoped<AuthRepository>();
            
        return this;
    }

    public ServiceConfiguration Configuration(IConfiguration configuration)
    {
        _configuration = configuration;
        
        return this;
    }
}