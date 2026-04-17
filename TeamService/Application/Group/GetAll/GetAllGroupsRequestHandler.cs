using Application.Common;
using Core;
using Domain.RepositoryInterfaces;
using MediatR;

namespace Application.Group.GetAll;

internal sealed class GetAllGroupsRequestHandler(IGroupRepository groupRepository)
    : IRequestHandler<GetAllGroupsRequest, Result<GetAllGroupsResponse>>
{
    private readonly IGroupRepository _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));

    public async Task<Result<GetAllGroupsResponse>> Handle(GetAllGroupsRequest request, CancellationToken cancellationToken)
    {
        var groups = await _groupRepository.GetAllAsync(cancellationToken);
        if (groups is null)
        {
            return Result<GetAllGroupsResponse>.Failure(ApplicationErrors.NotFound);
        }

        var groupDtos = groups.Select(group => new GetAllGroupsDto
        {
            GroupName = group.GroupName,
            PublicId = group.PublicId,
            Teams = [.. group.Teams.Select(team => team.TeamName)]
        }).ToList();

        var response = new GetAllGroupsResponse(groupDtos);

        return Result<GetAllGroupsResponse>.Success(response);
    }
}
