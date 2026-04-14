using Application.Common;

namespace Application.Match.GetMatches;

public sealed class GetMatchesResponse
{
    public List<GetMatchesDto> Matches { get; set; } = [];
}
