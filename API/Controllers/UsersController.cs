using API.Users;
using Application.Users.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController(ISender mediator) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateOrGetUser([FromBody] CreateUserRequest request)
    {
        var command = new CreateUserCommand(request.UserName);
        var user = await mediator.Send(command);
        return Ok(user);
    }
}