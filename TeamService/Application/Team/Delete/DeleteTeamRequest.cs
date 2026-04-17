using Core;
using MediatR;

namespace Application.Team.Delete;

public sealed class DeleteTeamRequest : IRequest<Result<DeleteTeamResponse>>
{
    public Guid PublicId { get; set; }
}