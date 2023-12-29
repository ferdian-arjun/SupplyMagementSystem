using API.Configurations;
using API.Interface;
using API.Repositories;
using API.Services;


var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddScoped<UserService>();

// Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();



//error handler
// builder.Services.AddControllers()
//     .ConfigureApiBehaviorOptions(options =>
//     {
//         options.InvalidModelStateResponseFactory = context =>
//         {
//             var errors = context.ModelState
//                 .Values
//                 .SelectMany(x => x.Errors)
//                 .Select(p => p.ErrorMessage)
//                 .ToArray();
//
//             return new BadRequestObjectResult(new ResponseErrorsHandler
//             {
//                 Code = StatusCodes.Status400BadRequest,
//                 Status = HttpStatusCode.BadRequest.ToString(),
//                 Message = "Validation Error",
//                 Errors = errors
//             });
//         };
//     });

builder.Services.AddHttpContextAccessor();

//Register IHTTPClientFactory
builder.Services.AddHttpClient();


//Database
// configure services, database, and cors
new EnvironmentConfiguration()
    .Service(builder.Services)
    .Configuration(builder.Configuration)
    .DbConnection()
    .UseCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowOrigin");

app.MapControllers();

app.UseHttpsRedirection();


app.Run();