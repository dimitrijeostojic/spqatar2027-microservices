namespace Domain.Entities;

public sealed class Group : Entity
{
    public string GroupName { get; private set; } = string.Empty;
    public ICollection<Team> Teams { get; set; } = [];

    public static Group Create(string groupName)
    {
        if (string.IsNullOrWhiteSpace(groupName))
            throw new ArgumentException("Group name is required.", nameof(groupName));

        return new Group { GroupName = groupName };
    }

    public Group UpdateGroupName(string groupName)
    {
        GroupName = groupName;
        return this;
    }
}
