using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastracture.RepositoryImplementations;

public sealed class StadiumRepository : IStadiumRepository
{

    private readonly IMongoCollection<Stadium> _collection;

    public StadiumRepository(IOptions<MongoDbOptions> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        var database = client.GetDatabase(options.Value.DatabaseName);
        _collection = database.GetCollection<Stadium>(options.Value.CollectionName);
    }


    public async Task<List<Stadium>> GetAllStadiumsAsync(CancellationToken cancellationToken)
    {
        return await _collection
          .Find(_ => true)
          .ToListAsync(cancellationToken);
    }

    public async Task<Stadium?> GetByPublicIdAsync(Guid stadiumPublicId, CancellationToken cancellationToken)
    {
        return await _collection
            .Find(s => s.PublicId == stadiumPublicId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
