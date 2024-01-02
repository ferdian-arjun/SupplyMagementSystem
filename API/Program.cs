using System.Net;
using API.Configurations;
using API.Interface;
using API.Repositories;
using API.Services;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CompanyService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<VendorService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<UserRoleService>();
builder.Services.AddScoped<ProjectVendorService>();

// Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IProjectVendorRepository, ProjectVendorRepository>();


//error Controller
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Values
                .SelectMany(x => x.Errors)
                .Select(p => p.ErrorMessage)
                .ToArray();

            return new BadRequestObjectResult(new ResponseErrorsHandler
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Validation Error",
                Errors = errors
            });
        };
    });

builder.Services.AddHttpContextAccessor();

//Register IHTTPClientFactory
builder.Services.AddHttpClient();

//JWT
new JwtConfiguration()
    .Configuration(builder.Configuration)
    .Authentication(builder.Services);


//Database
new EnvironmentConfiguration()
    .Service(builder.Services)
    .Configuration(builder.Configuration)
    .DbConnection()
    .UseCors();

//Swagger
new SwaggerConfiguration()
    .Configuration(builder.Configuration)
    .SwaggerGen(builder.Services);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UsePathBase(new PathString("/api/v1"));
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowOrigin");
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader());

app.MapControllers();

app.Run();