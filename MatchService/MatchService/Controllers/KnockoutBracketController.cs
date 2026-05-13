using Application.Knockout.CreateKnockoutBracket;
using Application.Knockout.GetKnockoutBracket;
using Application.Knockout.RecordKnockoutResult;
using Application.Knockout.ScheduleKnockoutMatch;
using Core;
using MatchService.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatchService.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class KnockoutBracketController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> CreateKnockoutBracket([FromBody] CreateKnockoutBracketRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.ToActionResult();
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetKnockoutBracket([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetKnockoutBracketRequest { BracketPublicId = id };
        var result = await _mediator.Send(request, cancellationToken);
        return result.ToActionResult();
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("{bracketId:guid}/matches/{matchId:guid}/schedule")]
    public async Task<IActionResult> ScheduleKnockoutMatch([FromRoute] Guid matchId, [FromBody] ScheduleKnockoutMatchRequest request, CancellationToken cancellationToken)
    {
        request.MatchPublicId = matchId;
        var result = await _mediator.Send(request, cancellationToken);
        return result.ToActionResult();
    }

    [Authorize(Roles = $"{Roles.Admin},{Roles.Manager}")]
    [HttpPost("{bracketId:guid}/matches/{matchId:guid}/result")]
    public async Task<IActionResult> RecordKnockoutResult([FromRoute] Guid bracketId, [FromRoute] Guid matchId, [FromBody] RecordKnockoutResultRequest request, CancellationToken cancellationToken)
    {
        request.BracketPublicId = bracketId;
        request.MatchPublicId = matchId;
        var result = await _mediator.Send(request, cancellationToken);
        return result.ToActionResult();
    }
}
