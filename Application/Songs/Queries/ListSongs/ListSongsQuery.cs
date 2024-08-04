using Domain.Songs;
using ErrorOr;
using MediatR;

namespace Application.Songs.Queries.ListSongs;

public record ListSongsQuery(string SearchQuery) : IRequest<ErrorOr<IReadOnlyList<Song>>>;
