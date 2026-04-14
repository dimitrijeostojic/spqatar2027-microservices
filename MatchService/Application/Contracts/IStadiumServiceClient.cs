using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts;

public interface IStadiumServiceClient
{
    Task<StadiumValidationResponse?> GetStadiumByPublicIdAsync(Guid publicId, CancellationToken cancellationToken = default);
}