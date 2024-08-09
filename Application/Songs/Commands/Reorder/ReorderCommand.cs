using MediatR;

namespace Application.Songs.Commands.Reorder;

public record ReorderCommand(IList<Reorder> Reorders) : IRequest;