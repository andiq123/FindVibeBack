using MediatR;

namespace Application.Stream.Commands;

public record GetStreamCommand(string songLink) : IRequest<string>;
