using Application.Common;
using Core;
using Domain.Abstractions;
using Domain.RepositoryInterfaces;
using MediatR;

namespace Application.Team.Create;

internal sealed class CreateTeamRequestHandler(
    ITeamRepository teamRepository,
    IGroupRepository groupRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateTeamRequest, Result<CreateTeamResponse>>
{
    public async Task<Result<CreateTeamResponse>> Handle(
        CreateTeamRequest request,
        CancellationToken cancellationToken)
    {
        var group = await groupRepository.GetByPublicIdAsync(request.GroupPublicId, cancellationToken);
        if (group is null)
            return Result<CreateTeamResponse>.Failure(ApplicationErrors.NotFound);

        var team = Domain.Entities.Team.Create(request.TeamName, request.FlagIcon, group);
        await teamRepository.AddAsync(team, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreateTeamResponse>.Success(new CreateTeamResponse
        {
            PublicId = team.PublicId,
            TeamName = team.TeamName,
            FlagIcon = team.FlagIcon
        });
    }
}