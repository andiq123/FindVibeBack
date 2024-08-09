using Application.Common;
using MediatR;

namespace Application.Songs.Commands.Reorder;

public class ReorderCommandHandler(ISongsRepository songsRepository, IUnitOfWork unitOfWork) : IRequestHandler<ReorderCommand>
{
    public async Task Handle(ReorderCommand request, CancellationToken cancellationToken)
    {
        await songsRepository.ReorderSongsAsync(request.Reorders);
        await unitOfWork.CommitChangesAsync();
    }
}