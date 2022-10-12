using ChessWebApp.Api.Contracts.Responses;
using ChessWebApp.Api.Database;
using ChessWebApp.Api.Repositories;
using ChessWebApp.Api.Services;
using ChessWebApp.Api.Validation;
using FastEndpoints;
using FastEndpoints.Swagger;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();

builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
    new SqliteConnectionFactory(builder.Configuration.GetValue<string>("Database:ConnectionString")));
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserService, UserService>();

WebApplication app = builder.Build();

app.UseCors();
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
