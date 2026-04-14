using Application.Match.CreateMatch;
using Application.Match.ForfeitMatch;
using Application.Match.GetMatches;
using Application.Match.RecordMatchResult;
using MatchService.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchService.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MatchController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost]
    public async Task<IActionResult> CreateMatch(CreateMatchRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.ToActionResult();
    }

    [HttpPut("{publicId}/result")]
    public async Task<IActionResult> RecordResult(Guid publicId, RecordMatchResultRequest request)
    {
        request.MatchPublicId = publicId;
        var result = await _mediator.Send(request);
        return result.ToActionResult();
    }

    [HttpPut("{publicId}/forfeit")]
    public async Task<IActionResult> Forfeit(Guid publicId, ForfeitMatchRequest request)
    {
        request.MatchPublicId = publicId;
        var result = await _mediator.Send(request);
        return result.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetMatchesRequest request)
    {
        var result = await _mediator.Send(request);
        return result.ToActionResult();
    }
}
