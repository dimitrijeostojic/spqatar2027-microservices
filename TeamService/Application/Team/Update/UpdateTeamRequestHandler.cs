using Application.Common;
using Core;
using Domain.Abstractions;
using Domain.RepositoryInterfaces;
using MediatR;

namespace Application.Team.Update;

internal sealed class UpdateTeamRequestHandler(
    ITeamRepository teamRepository,
    IGroupRepository groupRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateTeamRequest, Result<UpdateTeamResponse>>
{
    public async Task<Result<UpdateTeamResponse>> Handle(
        UpdateTeamRequest request,
        CancellationToken cancellationToken)
    {
        var team = await teamRepository.GetByPublicIdAsync(request.TeamPublicId!.Value, cancellationToken);
        if (team is null)
            return Result<UpdateTeamResponse>.Failure(ApplicationErrors.NotFound);

        if (request.GroupPublicId.HasValue)
        {
            var group = await groupRepository.GetByPublicIdAsync(request.GroupPublicId.Value, cancellationToken);
            if (group is null)
                return Result<UpdateTeamResponse>.Failure(ApplicationErrors.NotFound);

            team.UpdateGroup(group.Id);
        }

        if (!string.IsNullOrEmpty(request.TeamName))
            team.UpdateTeamName(request.TeamName);

        if (!string.IsNullOrEmpty(request.FlagIcon))
            team.UpdateFlagIcon(request.FlagIcon);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<UpdateTeamResponse>.Success(new UpdateTeamResponse
        {
            PublicId = team.PublicId,
            TeamName = team.TeamName,
            FlagIcon = team.FlagIcon,
            GroupName = team.Group?.GroupName ?? string.Empty
        });
    }
}
