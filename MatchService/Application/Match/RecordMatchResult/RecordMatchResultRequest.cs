using Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Match.RecordMatchResult;

public sealed class RecordMatchResultRequest : IRequest<Result<RecordMatchResultResponse>>
{
    public Guid? MatchPublicId { get; set; }
    public int HomePoints { get; set; }
    public int AwayPoints { get; set; }
}