using Application.Common;
using Application.Contracts;
using Application.Knockout.GetKnockoutBracket;
using Core;
using Domain.Abstraction;
using Domain.RepositoryInterfaces;
using MediatR;

namespace Application.Knockout.ScheduleKnockoutMatch;

internal sealed class ScheduleKnockoutMatchRequestHandler(
    IKnockoutBracketRepository bracketRepository,
    IStadiumServiceClient stadiumServiceClient,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ScheduleKnockoutMatchRequest, Result<ScheduleKnockoutMatchResponse>>
{
    private readonly IKnockoutBracketRepository _bracketRepository = bracketRepository ?? throw new ArgumentNullException(nameof(bracketRepository));
    private readonly IStadiumServiceClient _stadiumServiceClient = stadiumServiceClient ?? throw new ArgumentNullException(nameof(stadiumServiceClient));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<Result<ScheduleKnockoutMatchResponse>> Handle(
        ScheduleKnockoutMatchRequest request,
        CancellationToken cancellationToken)
    {
        var bracket = await _bracketRepository.GetByMatchPublicIdAsync(request.MatchPublicId, cancellationToken);

        if (bracket is null)
            return Result<ScheduleKnockoutMatchResponse>.Failure(ApplicationErrors.NotFound);

        bracket.ScheduleMatch(request.MatchPublicId, request.ScheduledDateTime, request.StadiumPublicId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var match = bracket.Matches.First(m => m.PublicId == request.MatchPublicId);

        string? stadiumName = null;
        if (match.StadiumPublicId.HasValue)
        {
            var stadium = await _stadiumServiceClient.GetStadiumByPublicIdAsync(match.StadiumPublicId.Value, cancellationToken);
            stadiumName = stadium?.StadiumName;
        }

        return Result<ScheduleKnockoutMatchResponse>.Success(new ScheduleKnockoutMatchResponse
        {
            Match = new KnockoutMatchDto
            {
                PublicId = match.PublicId,
                Round = match.Round,
                MatchOrder = match.MatchOrder,
                HomeTeamPublicId = match.HomeTeamPublicId,
                HomeTeamName = match.HomeTeamName,
                AwayTeamPublicId = match.AwayTeamPublicId,
                AwayTeamName = match.AwayTeamName,
                HomePoints = match.HomePoints,
                AwayPoints = match.AwayPoints,
                WinnerPublicId = match.WinnerPublicId,
                StadiumName = stadiumName,
                ScheduledAt = match.ScheduledAt,
                Status = match.Status
            }
        });
    }
}
