using Application.Common;
using Domain.Users;
using ErrorOr;
using MediatR;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateUserCommand, User>
{
    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.GetUserByUserNameAsync(request.UserName);
        if (existingUser != null)
        {
            return existingUser;
        }
        var user = await userRepository.CreateUserAsync(request.UserName);

        await unitOfWork.CommitChangesAsync();
        return user;
    }
}