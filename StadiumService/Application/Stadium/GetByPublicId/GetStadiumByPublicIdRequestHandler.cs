using Application.Common;
using Core;
using Domain.RepositoryInterfaces;
using MediatR;

namespace Application.Stadium.GetByPublicId;

public sealed class GetStadiumByPublicIdRequestHandler(IStadiumRepository stadiumRepository)
    : IRequestHandler<GetStadiumByPublicIdRequest, Result<GetStadiumByPublicIdResponse>>
{
    public async Task<Result<GetStadiumByPublicIdResponse>> Handle(
        GetStadiumByPublicIdRequest request,
        CancellationToken cancellationToken)
    {
        var stadium = await stadiumRepository.GetByPublicIdAsync(request.PublicId, cancellationToken);

        if (stadium is null)
            return Result<GetStadiumByPublicIdResponse>.Failure(ApplicationErrors.NotFound);

        return Result<GetStadiumByPublicIdResponse>.Success(new GetStadiumByPublicIdResponse
        {
            PublicId = stadium.PublicId,
            StadiumName = stadium.StadiumName,
            City = stadium.City,
            Capacity = stadium.Capacity
        });
    }
}