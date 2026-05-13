using Core;
using MediatR;

namespace Application.Knockout.CreateKnockoutBracket;

public sealed class CreateKnockoutBracketRequest : IRequest<Result<CreateKnockoutBracketResponse>>
{
    public List<Guid> SeededTeamPublicIds { get; set; } = [];
}
