using Application.Common;
using Core;
using Domain.RepositoryInterfaces;
using MediatR;

namespace Application.Team.GetById;

internal sealed class GetTeamByIdRequestHandler(ITeamRepository teamRepository)
    : IRequestHandler<GetTeamByIdRequest, Result<GetTeamByIdResponse>>
{
    public async Task<Result<GetTeamByIdResponse>> Handle(
        GetTeamByIdRequest request,
        CancellationToken cancellationToken)
    {
        var team = await teamRepository.GetByPublicIdAsync(request.PublicId, cancellationToken);
        if (team is null)
            return Result<GetTeamByIdResponse>.Failure(ApplicationErrors.NotFound);

        return Result<GetTeamByIdResponse>.Success(new GetTeamByIdResponse
        {
            PublicId = team.PublicId,
            TeamName = team.TeamName,
            FlagIcon = team.FlagIcon,
            GroupName = team.Group?.GroupName ?? string.Empty,
            GroupPublicId = team.Group?.PublicId,
            Exists = true
        });
    }
}