using Application.Common;
using Domain.Songs;
using Domain.Users;
using Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Users.Persistence;

public class UserRepository(FindVibeDbContext dbContext) : IUserRepository
{
    public async Task<User> CreateUserAsync(string userName)
    {
        var user = new User { Id = Guid.NewGuid(), Name = userName, CreatedAt = DateTime.UtcNow };
        await dbContext.Users.AddAsync(user);

        return user;
    }

    public async Task<User?> GetUserByUserNameAsync(string userName)
    {
        return await dbContext.Users.FirstOrDefaultAsync(x => x.Name == userName);
    }

    public async Task<User?> GetUserByIdWithSongsById(Guid userId)
    {
        return await dbContext.Users.Include(x => x.Songs).FirstOrDefaultAsync(x => x.Id == userId);
    }
}