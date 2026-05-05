using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Infrastructure.RepositoryImplementations;

public sealed class RefreshTokenRepository(AuthDbContext context) : IRefreshTokenRepository
{
    private const int _expirationDays = 7;

    public async Task<RefreshToken> CreateAsync(string userId, CancellationToken cancellationToken = default)
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            UserId = userId,
            ExpiresAt = DateTime.UtcNow.AddDays(_expirationDays),
            IsRevoked = false,
            CreatedAt = DateTime.UtcNow
        };

        context.RefreshTokens.Add(refreshToken);
        await context.SaveChangesAsync(cancellationToken);

        return refreshToken;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return await context.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == token, cancellationToken);
    }

    public async Task RevokeAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        refreshToken.IsRevoked = true;
        await context.SaveChangesAsync(cancellationToken);
    }
}
