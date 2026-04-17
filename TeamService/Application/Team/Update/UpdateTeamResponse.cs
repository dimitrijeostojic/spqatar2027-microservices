namespace Application.Team.Update;

public sealed class UpdateTeamResponse
{
    public Guid PublicId { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public string? FlagIcon { get; set; }
    public string GroupName { get; set; } = string.Empty;
}
