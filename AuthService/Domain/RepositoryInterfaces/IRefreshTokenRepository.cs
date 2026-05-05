using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> CreateAsync(string userId, CancellationToken cancellationToken = default);
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
    Task RevokeAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
}
