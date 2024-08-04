using Domain.Songs;

namespace Application.Common;

public interface ISongsRepository
{
    Task<IReadOnlyList<Song>> GetSongsAsync(string searchQuery);
    Task<IReadOnlyList<Song>> GetSongsForUsersAsync(Guid userId);
    Task AddSongToUserAsync(Song song);
    Task RemoveSongFromUserAsync(Guid songId);
}