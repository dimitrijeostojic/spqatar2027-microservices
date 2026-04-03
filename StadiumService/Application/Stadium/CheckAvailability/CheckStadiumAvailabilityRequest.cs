using Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stadium.CheckAvailability;

public sealed class CheckStadiumAvailabilityRequest : IRequest<Result<CheckStadiumAvailabilityResponse>>
{
    public Guid PublicId { get; set; }
}
