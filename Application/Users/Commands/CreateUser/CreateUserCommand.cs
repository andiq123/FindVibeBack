using Domain.Users;
using ErrorOr;
using MediatR;

namespace Application.Users.Commands.CreateUser;

public record CreateUserCommand(string UserName) : IRequest<User>;