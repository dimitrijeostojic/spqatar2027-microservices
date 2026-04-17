namespace Application.Group.GetAll;

public class GetAllGroupsDto
{
    public Guid PublicId { get; set; }
    public string GroupName { get; set; } = string.Empty;
    public List<string> Teams { get; set; } = [];
}