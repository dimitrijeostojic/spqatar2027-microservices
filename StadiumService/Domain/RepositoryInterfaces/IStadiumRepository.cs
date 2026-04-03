using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces;

public interface IStadiumRepository
{
    Task<List<Stadium>> GetAllStadiumsAsync(CancellationToken cancellationToken);
    Task<Stadium?> GetByPublicIdAsync(Guid stadiumPublicId, CancellationToken cancellationToken);
}
