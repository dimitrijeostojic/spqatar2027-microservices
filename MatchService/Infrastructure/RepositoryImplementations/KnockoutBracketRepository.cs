using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImplementations;

public sealed class KnockoutBracketRepository(ApplicationDbContext context) : IKnockoutBracketRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<KnockoutBracket?> GetByPublicIdAsync(Guid publicId, CancellationToken cancellationToken = default)
        => await _context.KnockoutBrackets
            .Include(b => b.Matches)
            .FirstOrDefaultAsync(b => b.PublicId == publicId, cancellationToken);

    public async Task<KnockoutBracket?> GetByMatchPublicIdAsync(Guid matchPublicId, CancellationToken cancellationToken = default)
        => await _context.KnockoutBrackets
            .Include(b => b.Matches)
            .FirstOrDefaultAsync(b => b.Matches.Any(m => m.PublicId == matchPublicId), cancellationToken);

    public async Task AddAsync(KnockoutBracket bracket, CancellationToken cancellationToken = default)
        => await _context.KnockoutBrackets.AddAsync(bracket, cancellationToken);

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _context.KnockoutBrackets.CountAsync(cancellationToken);
    }
}
