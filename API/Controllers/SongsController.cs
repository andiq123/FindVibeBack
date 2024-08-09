using API.Songs;
using Application.Songs.Commands.AddFavorite;
using Application.Songs.Commands.RemoveFavorite;
using Application.Songs.Commands.Reorder;
using Application.Songs.Queries.ListSongs;
using Application.Songs.Queries.ListSongsForUser;
using Domain.Songs;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SongsController(ISender mediator) : ApiController
{
    [HttpGet("search")]
    public async Task<IActionResult> SearchSongs([FromQuery] SearchSongsRequest request)
    {
        var listSongsQuery = new ListSongsQuery(request.SearchQuery);
        var songsResult = await mediator.Send(listSongsQuery);
        return songsResult.MatchFirst<IActionResult>(
            songs => Ok(new SongsResponse(mapSongDto(songs))),
            error => NotFound(error.Description));
    }

    [HttpGet("get-favorites")]
    public async Task<IActionResult> GetFavoriteSongs([FromQuery] GetFavoriteSongsRequest request)
    {
        var listFavoriteSongsQuery = new ListSongsForUserQuery(request.UserId);
        var favoriteSongsResult = await mediator.Send(listFavoriteSongsQuery);
        return favoriteSongsResult.MatchFirst<IActionResult>(
            favoriteSongs => Ok(new SongsResponse(mapSongDto(favoriteSongs))),
            error => NotFound(error.Description));
    }
    
    [HttpPost("reorder")]
    public async Task<IActionResult> ReorderSongs([FromBody] ReorderRequest request)
    {
        var reorderCommand = new ReorderCommand(request.Reorders);
        await mediator.Send(reorderCommand);
        return Ok();
    }

    [HttpPost("add-favorite")]
    public async Task<IActionResult> AddSongToFavorite([FromBody] AddToFavoriteRequest request)
    {
        var addFavoriteCommand = new AddFavoriteCommand(request.Title, request.Artist, request.Image, request.Link, request.Order, request.UserId);
        await mediator.Send(addFavoriteCommand);
        return Ok();
    }

    [HttpDelete("remove-favorite")]
    public async Task<IActionResult> RemoveSongFromFavorite([FromQuery] RemoveFromFavoriteRequest request)
    {
        var removeFavoriteCommand = new RemoveFavoriteCommand(request.SongId);
        await mediator.Send(removeFavoriteCommand);
        return Ok();
    }

    private IReadOnlyList<SongDto> mapSongDto(IReadOnlyList<Song> songs)
    {
        return songs.Select(x => new SongDto()
        {
            Id = x.Id,
            Title = x.Title,
            Artist = x.Artist,
            Image = x.Image,
            Order = x.Order,
            Link = x.Link
        }).ToList();
    }
}