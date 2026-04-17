using Application.Common.Collection;

namespace Application.Team.GetAll;

public sealed class GetAllTeamsResponse(IList<GetAllTeamsDto> teams)
    : EntityCollectionResult<GetAllTeamsDto>(teams)
{
}
