using MediatR;

namespace Application.Songs.Commands.RemoveFavorite;

public record RemoveFavoriteCommand(Guid SongId) : IRequest;