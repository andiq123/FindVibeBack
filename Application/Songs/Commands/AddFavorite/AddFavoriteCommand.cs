using Domain.Songs;
using MediatR;

namespace Application.Songs.Commands.AddFavorite;

public record AddFavoriteCommand(string Title, string Artist, string Image, string Link, int Order, Guid UserId)
    : IRequest;