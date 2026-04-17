using Application.Events;
using Domain.Abstractions;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using MassTransit;

namespace Application.EventConsumers;

public sealed class MatchCompletedEventConsumer(
    ITeamStandingRepository standingRepository,
    IUnitOfWork unitOfWork)
    : IConsumer<MatchCompletedEvent>
{
    public async Task Consume(ConsumeContext<MatchCompletedEvent> context)
    {
        var evt = context.Message;

        await ApplyStandingsAsync(
            evt.HomeTeamPublicId,
            evt.HomePlayed, evt.HomeWins, evt.HomeDraws, evt.HomeLosses,
            evt.HomePointsFor, evt.HomePointsAgainst, evt.HomeStandingPoints,
            context.CancellationToken);

        await ApplyStandingsAsync(
            evt.AwayTeamPublicId,
            evt.AwayPlayed, evt.AwayWins, evt.AwayDraws, evt.AwayLosses,
            evt.AwayPointsFor, evt.AwayPointsAgainst, evt.AwayStandingPoints,
            context.CancellationToken);

        await unitOfWork.SaveChangesAsync(context.CancellationToken);
    }

    private async Task ApplyStandingsAsync(
        Guid teamPublicId,
        int played, int wins, int draws, int losses,
        int pointsFor, int pointsAgainst, int standingPoints,
        CancellationToken cancellationToken)
    {
        var standing = await standingRepository.GetByTeamPublicIdAsync(teamPublicId, cancellationToken);

        if (standing is null)
        {
            standing = TeamStanding.Create(teamPublicId);
            await standingRepository.AddAsync(standing, cancellationToken);
        }

        standing.Apply(played, wins, draws, losses, pointsFor, pointsAgainst, standingPoints);
    }
}