using Application.Common;
using Core;
using Domain.Abstractions;
using Domain.RepositoryInterfaces;
using MediatR;

namespace Application.Team.Delete;

internal sealed class DeleteTeamRequestHandler(
    ITeamRepository teamRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteTeamRequest, Result<DeleteTeamResponse>>
{
    public async Task<Result<DeleteTeamResponse>> Handle(
        DeleteTeamRequest request,
        CancellationToken cancellationToken)
    {
        var team = await teamRepository.GetByPublicIdAsync(request.PublicId, cancellationToken);
        if (team is null)
            return Result<DeleteTeamResponse>.Failure(ApplicationErrors.NotFound);

        teamRepository.Delete(team);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<DeleteTeamResponse>.Success(new DeleteTeamResponse
        {
            PublicId = team.PublicId,
            TeamName = team.TeamName
        });
    }
}