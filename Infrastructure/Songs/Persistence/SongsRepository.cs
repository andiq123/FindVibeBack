using Application.Common;
using Domain.Songs;
using Infrastructure.Common.Persistence;
using Infrastructure.Songs.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Songs.Persistence;

public class SongsRepository(
    SongsScrapperService songsScrapperService,
    FindVibeDbContext context,
    IUserRepository userRepository) : ISongsRepository
{
    public async Task<IReadOnlyList<Song>> GetSongsAsync(string searchQuery)
    {
        searchQuery = searchQuery.Replace(" ", "-");
        return await songsScrapperService.ScrapeSongsByQueryAsync(searchQuery);
    }

    public async Task AddSongToUserAsync(Song song)
    {
        var user = await userRepository.GetUserByIdWithSongsById(song.UserId);
        var songExists = user != null && user.Songs.Any(x => x.Link == song.Link);
        if (songExists) return;
        await context.Songs.AddAsync(song);
    }

    public async Task<IReadOnlyList<Song>> GetSongsForUsersAsync(Guid userId)
    {
        var user = await userRepository.GetUserByIdWithSongsById(userId);
        return user == null ? [] : user.Songs.ToList();
    }


    public async Task RemoveSongFromUserAsync(Guid songId)
    {
        var song = await context.Songs.FindAsync(songId);
        if (song != null) context.Songs.Remove(song);
    }
}