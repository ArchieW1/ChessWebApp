using System.Text;
using ChessWebApp.Api.Authentication;
using ChessWebApp.Api.Contracts.Responses;
using ChessWebApp.Api.Database;
using ChessWebApp.Api.Repositories;
using ChessWebApp.Api.Services;
using ChessWebApp.Api.Validation;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string uiHttpsUrl = builder.Configuration.GetValue<string>("Url:UiHttps");
string uiHttpUrl = builder.Configuration.GetValue<string>("Url:UiHttp");
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder => 
        policyBuilder.WithOrigins(uiHttpsUrl, uiHttpUrl)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuers = new []
        {
            builder.Configuration.GetValue<string>("Jwt:Issuers:Https"),
            builder.Configuration.GetValue<string>("Jwt:Issuers:Http")
        },
        ValidAudiences = new [] {uiHttpsUrl, uiHttpUrl},
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration.GetValue<string>("Jwt:Key"))),
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();

builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
    new SqliteConnectionFactory(builder.Configuration.GetValue<string>("Database:ConnectionString")));
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IGameRepository, GameRepository>();
builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddSingleton<LoginAuthentication>();

WebApplication app = builder.Build();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseFastEndpoints(config =>
{
    config.ErrorResponseBuilder = (failures, _) =>
    {
        return new ValidationFailureResponse
        {
            Errors = failures.Select(failure => failure.ErrorMessage).ToList()
        };
    };
});

app.UseOpenApi();
app.UseSwaggerUi3(settings => settings.ConfigureDefaults());

DatabaseInitializer databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
await databaseInitializer.InitializeAsync();

app.Run();
