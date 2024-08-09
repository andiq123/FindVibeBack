using Application.Common;
using Domain.Songs;
using MediatR;

namespace Application.Songs.Commands.AddFavorite;

public class AddFavoriteCommandHandler(
    ISongsRepository songsRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<AddFavoriteCommand>
{
    public async Task Handle(AddFavoriteCommand request, CancellationToken cancellationToken)
    {
        var song = new Song
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Artist = request.Artist,
            Image = request.Image,
            Link = request.Link,
            Order = request.Order,
            UserId = request.UserId
        };
        await songsRepository.AddSongToUserAsync(song);
        await unitOfWork.CommitChangesAsync();
    }
}