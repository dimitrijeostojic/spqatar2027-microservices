using Application.Common;
using Core;
using Domain.RepositoryInterfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;

namespace Application.RefreshToken;

public sealed class RefreshTokenRequestHandler(
    IRefreshTokenRepository refreshTokenRepository,
    IJwtTokenRepository jwtTokenRepository,
    UserManager<User> userManager
    )
    : IRequestHandler<RefreshTokenRequest, Result<RefreshTokenResponse>>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository));
    private readonly IJwtTokenRepository _jwtTokenRepository = jwtTokenRepository ?? throw new ArgumentNullException(nameof(jwtTokenRepository));
    private readonly UserManager<User> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

    public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var existingToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);

        if (existingToken is null || existingToken.IsRevoked || existingToken.ExpiresAt <= DateTime.UtcNow)
        {
            return Result<RefreshTokenResponse>.Failure(ApplicationErrors.InvalidRefreshToken);
        }

        // Token rotation — revoke old, issue new
        await _refreshTokenRepository.RevokeAsync(existingToken, cancellationToken);

        var roles = await _userManager.GetRolesAsync(existingToken.User);
        var newAccessToken = await _jwtTokenRepository.GenerateTokenAsync(existingToken.User, roles);
        var newRefreshToken = await _refreshTokenRepository.CreateAsync(existingToken.UserId, cancellationToken);

        return Result<RefreshTokenResponse>.Success(new RefreshTokenResponse(newAccessToken, newRefreshToken.Token));
    }
}
