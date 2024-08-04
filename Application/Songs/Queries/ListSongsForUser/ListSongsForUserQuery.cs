using Domain.Songs;
using MediatR;
using ErrorOr;

namespace Application.Songs.Queries.ListSongsForUser;

public record ListSongsForUserQuery(Guid UserId) : IRequest<ErrorOr<IReadOnlyList<Song>>>;
