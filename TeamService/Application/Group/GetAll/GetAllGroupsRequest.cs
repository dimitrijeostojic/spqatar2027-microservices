using Core;
using MediatR;

namespace Application.Group.GetAll;

public sealed record GetAllGroupsRequest : IRequest<Result<GetAllGroupsResponse>>;
