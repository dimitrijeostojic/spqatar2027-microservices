using Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stadium.GetByPublicId;

public sealed class GetStadiumByPublicIdRequest : IRequest<Result<GetStadiumByPublicIdResponse>>
{
    public Guid PublicId { get; set; }
}
