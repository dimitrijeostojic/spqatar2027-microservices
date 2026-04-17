namespace Application.Team.GetAll;

public sealed class GetAllTeamsDto
{
    public Guid PublicId { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public string? FlagIcon { get; set; }
    public string GroupName { get; set; } = string.Empty;
    public Guid? GroupPublicId { get; set; }
}
