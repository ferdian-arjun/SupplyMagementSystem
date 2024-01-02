using System.Configuration;
using System.Text;
using API.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SupplyManagementSystem.Configurations;
using SupplyManagementSystem.Controllers;
using SupplyManagementSystem.Repositories;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;


var service = builder.Services;

// Register IHttpClientFactory
service.AddHttpClient();

// Add services to the container.
service.AddControllersWithViews();
service.AddDistributedMemoryCache();
service.AddMvc();
service.AddMemoryCache();
service.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);//You can set Time   
});
service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

service.Configure<BaseUrls>(configuration.GetSection("ConnectionStrings"));

service.AddControllersWithViews();
service.AddScoped<AuthRepository>();
service.AddScoped<UserRepository>();

//JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Secret"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.Use(async (context, next) =>
{
    var JWToken = context.Session.GetString("JWToken");
    if (!string.IsNullOrEmpty(JWToken))
    {
        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
    }
    await next();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}");

// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapControllerRoute(
//         name: "default",
//         pattern: "{controller=Auth}/{action=Login}");
// });

app.Run();