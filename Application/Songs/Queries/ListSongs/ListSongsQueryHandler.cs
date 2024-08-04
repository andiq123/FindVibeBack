using Application.Common;
using Domain.Songs;
using ErrorOr;
using MediatR;

namespace Application.Songs.Queries.ListSongs;

public class ListSongsQueryHandler(ISongsRepository songsRepository) : IRequestHandler<ListSongsQuery, ErrorOr<IReadOnlyList<Song>>>
{
    public async Task<ErrorOr<IReadOnlyList<Song>>> Handle(ListSongsQuery request, CancellationToken cancellationToken)
    {
        var songs = await songsRepository.GetSongsAsync(request.SearchQuery);

        if (!songs.Any())
        {
            return Error.NotFound();
        }

        return songs.ToList();
    }
}