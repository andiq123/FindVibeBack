using Application.Common;
using Domain.Songs;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common.Persistence;

public class FindVibeDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Song> Songs { get; set; } = null!;

    public async Task CommitChangesAsync()
    {
        await base.SaveChangesAsync();
    }
}