using MediatR;

namespace Application.Stream.Commands;

public class GetStreamCommandHandler :IRequestHandler<GetStreamCommand, string>
{
    public Task<string> Handle(GetStreamCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}