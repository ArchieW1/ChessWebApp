using ChessWebApp.DbApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChessWebApp.DbApi.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
}