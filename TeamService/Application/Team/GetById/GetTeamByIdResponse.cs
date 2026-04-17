namespace Application.Team.GetById;

public sealed class GetTeamByIdResponse
{
    public Guid PublicId { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public string? FlagIcon { get; set; }
    public string GroupName { get; set; } = string.Empty;
    public Guid? GroupPublicId { get; set; }
    public bool Exists { get; set; }
}