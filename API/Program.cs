using System.Net;
using API.Configurations;
using API.Interface;
using API.Repositories;
using API.Services;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();

// Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();


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

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();