using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImplementations;

public sealed class TeamStandingRepository(ApplicationDbContext context) : ITeamStandingRepository
{
    public async Task<TeamStanding?> GetByTeamPublicIdAsync(
        Guid teamPublicId,
        CancellationToken cancellationToken = default)
        => await context.TeamStandings
            .FirstOrDefaultAsync(s => s.TeamPublicId == teamPublicId, cancellationToken);

    public async Task<List<TeamStanding>> GetByGroupPublicIdAsync(
        Guid groupPublicId,
        CancellationToken cancellationToken = default)
        => await context.TeamStandings
            .Where(s => context.Teams
                .Any(t => t.PublicId == s.TeamPublicId &&
                          t.Group!.PublicId == groupPublicId))
            .ToListAsync(cancellationToken);

    public async Task AddAsync(
        TeamStanding standing,
        CancellationToken cancellationToken = default)
        => await context.TeamStandings.AddAsync(standing, cancellationToken);
}
