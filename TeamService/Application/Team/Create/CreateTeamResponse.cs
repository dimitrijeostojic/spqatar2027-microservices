namespace Application.Team.Create;

public sealed class CreateTeamResponse
{
    public Guid PublicId { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public string? FlagIcon { get; set; }
}