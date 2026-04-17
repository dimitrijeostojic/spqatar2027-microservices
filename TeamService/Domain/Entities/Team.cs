namespace Domain.Entities;

public sealed class Team : Entity
{
    public string TeamName { get; private set; } = string.Empty;
    public string? FlagIcon { get; private set; }
    public int? GroupId { get; private set; }
    public Group? Group { get; private set; }

    public static Team Create(string teamName, string? flagIcon, Group group)
    {
        if (string.IsNullOrWhiteSpace(teamName))
            throw new ArgumentException("Team name is required.", nameof(teamName));

        return new Team
        {
            TeamName = teamName,
            FlagIcon = flagIcon,
            Group = group,
            GroupId = group.Id
        };
    }

    public Team UpdateTeamName(string teamName)
    {
        TeamName = teamName;
        return this;
    }

    public Team UpdateFlagIcon(string flagIcon)
    {
        FlagIcon = flagIcon;
        return this;
    }

    public Team UpdateGroup(int groupId)
    {
        GroupId = groupId;
        return this;
    }
}
