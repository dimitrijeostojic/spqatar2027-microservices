using Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Match.CreateMatch;

public sealed class CreateMatchRequest : IRequest<Result<CreateMatchResponse>>
{
    public Guid HomeTeamPublicId { get; set; }
    public Guid AwayTeamPublicId { get; set; }
    public Guid StadiumPublicId { get; set; }
    public DateTime StartTime { get; set; }
}
