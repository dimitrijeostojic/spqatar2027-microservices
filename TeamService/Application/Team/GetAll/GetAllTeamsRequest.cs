using Core;
using MediatR;

namespace Application.Team.GetAll;

public sealed class GetAllTeamsRequest
    : IRequest<Result<GetAllTeamsResponse>>
{
}
