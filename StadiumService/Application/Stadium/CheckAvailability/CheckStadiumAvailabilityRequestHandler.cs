using Core;
using Domain.RepositoryInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stadium.CheckAvailability;

public sealed class CheckStadiumAvailabilityRequestHandler(IStadiumRepository stadiumRepository)
    : IRequestHandler<CheckStadiumAvailabilityRequest, Result<CheckStadiumAvailabilityResponse>>
{
    public async Task<Result<CheckStadiumAvailabilityResponse>> Handle(
        CheckStadiumAvailabilityRequest request,
        CancellationToken cancellationToken)
    {
        var stadium = await stadiumRepository.GetByPublicIdAsync(request.PublicId, cancellationToken);

        return Result<CheckStadiumAvailabilityResponse>.Success(
            new CheckStadiumAvailabilityResponse
            {
                Exists = stadium is not null,
                PublicId = request.PublicId,
                StadiumName = stadium?.StadiumName ?? string.Empty
            });
    }
}