using Application.Common.Collection;

namespace Application.Group.GetAll;

public sealed class GetAllGroupsResponse(ICollection<GetAllGroupsDto> groups)
    : EntityCollectionResult<GetAllGroupsDto>(groups)
{
}
