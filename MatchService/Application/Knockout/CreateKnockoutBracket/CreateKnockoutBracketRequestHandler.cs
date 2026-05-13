using Application.Common;
using Application.Contracts;
using Core;
using Domain.Abstraction;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using MediatR;

namespace Application.Knockout.CreateKnockoutBracket;

public sealed class CreateKnockoutBracketRequestHandler(
    IKnockoutBracketRepository bracketRepository,
    ITeamServiceClient teamServiceClient,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateKnockoutBracketRequest, Result<CreateKnockoutBracketResponse>>
{
    private readonly IKnockoutBracketRepository _bracketRepository = bracketRepository ?? throw new ArgumentNullException(nameof(bracketRepository));
    private readonly ITeamServiceClient _teamServiceClient = teamServiceClient ?? throw new ArgumentNullException(nameof(teamServiceClient));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<Result<CreateKnockoutBracketResponse>> Handle(
        CreateKnockoutBracketRequest request,
        CancellationToken cancellationToken)
    {
        if (request.SeededTeamPublicIds.Count != 8)
        {
            return Result<CreateKnockoutBracketResponse>.Failure(ApplicationErrors.InvalidSeededTeamsCount);
        }

        if (request.SeededTeamPublicIds.Distinct().Count() != 8)
        {
            return Result<CreateKnockoutBracketResponse>.Failure(ApplicationErrors.DuplicateSeededTeams);
        }

        var numberOfBrackets = await _bracketRepository.CountAsync();
        if (numberOfBrackets > 0)
        {
            return Result<CreateKnockoutBracketResponse>.Failure(ApplicationErrors.BracketExists);
        }

        var seededTeams = new List<(Guid PublicId, string Name)>();

        foreach (var teamPublicId in request.SeededTeamPublicIds)
        {
            var team = await _teamServiceClient.GetTeamByPublicIdAsync(teamPublicId, cancellationToken);

            if (team is null || !team.Exists)
                return Result<CreateKnockoutBracketResponse>.Failure(ApplicationErrors.NotFound);

            seededTeams.Add((teamPublicId, team.TeamName));
        }

        var bracket = KnockoutBracket.Create(seededTeams);

        await _bracketRepository.AddAsync(bracket, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreateKnockoutBracketResponse>.Success(new CreateKnockoutBracketResponse
        {
            PublicId = bracket.PublicId
        });
    }
}
