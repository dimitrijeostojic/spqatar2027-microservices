namespace Application.Team.Delete;

public sealed class DeleteTeamResponse
{
    public Guid PublicId { get; set; }
    public string TeamName { get; set; } = string.Empty;
}