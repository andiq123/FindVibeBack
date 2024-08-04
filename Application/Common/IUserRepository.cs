using Domain.Users;

namespace Application.Common;

public interface IUserRepository
{
    Task<User> CreateUserAsync(string userName);
    Task<User?> GetUserByUserNameAsync(string userName);
    Task<User?> GetUserByIdWithSongsById(Guid userId);
}