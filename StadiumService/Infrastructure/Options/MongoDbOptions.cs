using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Options;

public sealed class MongoDbOptions
{
    public required string ConnectionString { get; init; }
    public required string DatabaseName { get; init; }
    public required string CollectionName { get; init; }
}
