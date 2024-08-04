using Application.Common;
using MediatR;

namespace Application.Songs.Commands.RemoveFavorite;

public class RemoveFavoriteCommandHandler(ISongsRepository songsRepository,IUnitOfWork unitOfWork) :IRequestHandler<RemoveFavoriteCommand>
{
    public async Task Handle(RemoveFavoriteCommand request, CancellationToken cancellationToken)
    {
        await songsRepository.RemoveSongFromUserAsync(request.SongId);
        await unitOfWork.CommitChangesAsync();
    }
}