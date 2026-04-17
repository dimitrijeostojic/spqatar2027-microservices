using Application.Common.Collection;

namespace Application.Group.GetGroupStandings;

public sealed class GetGroupStandingsResponse(List<StandingDto> groupStandings)
    : EntityCollectionResult<StandingDto>(groupStandings)
{
}
