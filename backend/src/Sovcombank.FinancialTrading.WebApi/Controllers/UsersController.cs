using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sovcombank.FinancialTrading.Application.UserProfiles.Commands;

namespace Sovcombank.FinancialTrading.WebApi.Controllers;

[ApiController, Route("api/[controller]")]
public sealed class UsersController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult<Guid>> Register(RegisterUserCommand command) =>
        await _mediator.Send(command);
}
