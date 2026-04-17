namespace Application.Group.Create;

public sealed class CreateGroupResponse
{
    public Guid PublicId { get; set; }
    public required string GroupName { get; set; }
}
