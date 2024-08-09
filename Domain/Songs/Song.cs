using Domain.Users;

namespace Domain.Songs;

public class Song
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Artist { get; init; }
    public string Image { get; init; }
    public string Link { get; init; }
    public int Order { get; set; }

    public Guid UserId { get; init; }
    public User User { get; init; }
}