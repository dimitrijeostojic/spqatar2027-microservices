using Core;
using MediatR;

namespace Application.Group.Create;

public sealed class CreateGroupRequest
    : IRequest<Result<CreateGroupResponse>>
{
    public required string GroupName { get; set; }
}
