using Application.Common;
using Application.Contracts;
using Core;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using MediatR;

namespace Application.Knockout.GetKnockoutBracket;

public sealed class GetKnockoutBracketRequestHandler(
    IKnockoutBracketRepository bracketRepository,
    IStadiumServiceClient stadiumServiceClient)
    : IRequestHandler<GetKnockoutBracketRequest, Result<GetKnockoutBracketResponse>>
{
    public async Task<Result<GetKnockoutBracketResponse>> Handle(
        GetKnockoutBracketRequest request,
        CancellationToken cancellationToken)
    {
        var bracket = await bracketRepository.GetByPublicIdAsync(request.BracketPublicId!.Value, cancellationToken);

        if (bracket is null)
            return Result<GetKnockoutBracketResponse>.Failure(ApplicationErrors.NotFound);

        var matchDtos = new List<KnockoutMatchDto>();

        foreach (var m in bracket.Matches.OrderBy(m => m.Round).ThenBy(m => m.MatchOrder))
        {
            string? stadiumName = null;
            if (m.StadiumPublicId.HasValue)
            {
                var stadium = await stadiumServiceClient.GetStadiumByPublicIdAsync(m.StadiumPublicId.Value, cancellationToken);
                stadiumName = stadium?.StadiumName;
            }

            matchDtos.Add(new KnockoutMatchDto
            {
                PublicId = m.PublicId,
                Round = m.Round,
                MatchOrder = m.MatchOrder,
                HomeTeamPublicId = m.HomeTeamPublicId,
                HomeTeamName = m.HomeTeamName,
                AwayTeamPublicId = m.AwayTeamPublicId,
                AwayTeamName = m.AwayTeamName,
                HomePoints = m.HomePoints,
                AwayPoints = m.AwayPoints,
                WinnerPublicId = m.WinnerPublicId,
                StadiumName = stadiumName,
                ScheduledAt = m.ScheduledAt,
                Status = m.Status
            });
        }

        return Result<GetKnockoutBracketResponse>.Success(new GetKnockoutBracketResponse(matchDtos));
    }
}
