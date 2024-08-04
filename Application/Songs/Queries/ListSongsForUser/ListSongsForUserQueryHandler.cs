using Application.Common;
using Domain.Songs;
using ErrorOr;
using MediatR;

namespace Application.Songs.Queries.ListSongsForUser;

public class ListSongsForUserQueryHandler(ISongsRepository songsRepository) : IRequestHandler<ListSongsForUserQuery, ErrorOr<IReadOnlyList<Song>>>
{
    public async Task<ErrorOr<IReadOnlyList<Song>>> Handle(ListSongsForUserQuery request, CancellationToken cancellationToken)
    {
        var songs = await songsRepository.GetSongsForUsersAsync(request.UserId);
        if (!songs.Any())
        {
            return Error.NotFound();
        }

        return songs.ToList();
    }
}