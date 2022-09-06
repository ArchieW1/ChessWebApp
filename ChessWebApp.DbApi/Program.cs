using AutoMapper;
using ChessWebApp.DbApi.DataAccess;
using ChessWebApp.DbApi.Models;
using ChessWebApp.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration["SqlDbConnectionString"];

builder.Services.AddDbContext<AppDbContext>(o => 
    o.UseSqlServer(connectionString, s => s.EnableRetryOnFailure()));
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("api/users", async (IUserRepo repo, IMapper mapper) =>
{
    IEnumerable<User> users = await repo.GetAllAsync();
    return Results.Ok(mapper.Map<IEnumerable<UserReadDto>>(users));
});

app.MapGet("api/users/id/{id}", async (IUserRepo repo, IMapper mapper, Guid id) =>
{
    User? user = await repo.GetByIdAsync(id);
    return user is null ? Results.NotFound() : Results.Ok(mapper.Map<UserReadDto>(user));
});

app.MapGet("api/users/username/{username}", async (IUserRepo repo, IMapper mapper, string username) =>
{
    User? user = await repo.GetByUsernameAsync(username);
    return user is null ? Results.NotFound() : Results.Ok(mapper.Map<UserReadDto>(user));
});

app.MapGet("api/users/email/{email}", async (IUserRepo repo, IMapper mapper, string email) =>
{
    User? user = await repo.GetByEmailAsync(email);
    return user is null ? Results.NotFound() : Results.Ok(mapper.Map<UserReadDto>(user));
});

app.MapPost("api/users", async (IUserRepo repo, IMapper mapper, UserCreateDto userCreateDto) =>
{
    User? userModel = mapper.Map<User>(userCreateDto);
    await repo.CreateAsync(userModel);
    await repo.SaveChangesAsync();
    UserReadDto userReadDto = mapper.Map<UserReadDto>(userModel);
    return Results.Created($"api/users/{userReadDto.Id.ToString()}", userReadDto);
});

app.MapPut("api/users/{id}", async (IUserRepo repo, IMapper mapper, Guid id, UserUpdateDto userUpdateDto) =>
{
    User? user = await repo.GetByIdAsync(id);
    if (user is null)
    {
        return Results.NotFound();
    }
    mapper.Map(userUpdateDto, user);
    await repo.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("api/users/{id}", async (IUserRepo repo, Guid id) =>
{
    User? user = await repo.GetByIdAsync(id);
    if (user is null)
    {
        return Results.NotFound();
    }
    repo.Delete(user);
    await repo.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();