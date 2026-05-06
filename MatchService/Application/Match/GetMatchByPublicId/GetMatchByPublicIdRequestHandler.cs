using Application.Common;
using Core;
using Domain.RepositoryInterfaces;
using MediatR;

namespace Application.Match.GetMatchByPublicId;

internal sealed class GetMatchByPublicIdRequestHandler(IMatchRepository matchRepository)
    : IRequestHandler<GetMatchByPublicIdRequest, Result<GetMatchByPublicIdResponse>>
{
    private readonly IMatchRepository _matchRepository = matchRepository ?? throw new ArgumentNullException(nameof(matchRepository));

    public async Task<Result<GetMatchByPublicIdResponse>> Handle(GetMatchByPublicIdRequest request, CancellationToken cancellationToken)
    {
        var match = await _matchRepository.GetByPublicIdAsync(request.MatchPublicId, cancellationToken);
        if (match is null)
            return Result<GetMatchByPublicIdResponse>.Failure(ApplicationErrors.NotFound);

        var response = new GetMatchByPublicIdResponse
        {
            PublicId = match.PublicId,
            HomeTeam = match.HomeTeamName,
            AwayTeam = match.AwayTeamName,
            Stadium = match.StadiumName,
            StartTime = match.StartTime,
            HomePoints = match.HomePoints,
            AwayPoints = match.AwayPoints,
            IsForfeit = match.IsForfeit,
            ForfeitLoser = match.ForfeitLoser,
            Status = match.Status
        };
        return Result<GetMatchByPublicIdResponse>.Success(response);
    }
}
