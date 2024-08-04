using Domain.Songs;

namespace Domain.Users;

public class User
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public DateTime CreatedAt { get; init; }
    public IList<Song> Songs { get; init; } = null!;
}