using Core;
using MediatR;

namespace Application.RefreshToken;

public sealed class RefreshTokenRequest : IRequest<Result<RefreshTokenResponse>>
{
    public string RefreshToken { get; set; } = string.Empty;
}
