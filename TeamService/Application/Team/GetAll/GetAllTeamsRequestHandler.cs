using Core;
using Domain.RepositoryInterfaces;
using MediatR;

namespace Application.Team.GetAll;

internal sealed class GetAllTeamsRequestHandler(ITeamRepository teamRepository)
    : IRequestHandler<GetAllTeamsRequest, Result<GetAllTeamsResponse>>
{
    public async Task<Result<GetAllTeamsResponse>> Handle(
        GetAllTeamsRequest request,
        CancellationToken cancellationToken)
    {
        var teams = await teamRepository.GetAllAsync(cancellationToken);

        var dtos = teams.Select(t => new GetAllTeamsDto
        {
            PublicId = t.PublicId,
            TeamName = t.TeamName,
            FlagIcon = t.FlagIcon,
            GroupName = t.Group?.GroupName ?? string.Empty,
            GroupPublicId = t.Group?.PublicId
        }).ToList();

        return Result<GetAllTeamsResponse>.Success(new GetAllTeamsResponse(dtos));
    }
}
