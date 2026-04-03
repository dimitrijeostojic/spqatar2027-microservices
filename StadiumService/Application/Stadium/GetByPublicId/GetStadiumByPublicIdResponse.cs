using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stadium.GetByPublicId;

public sealed class GetStadiumByPublicIdResponse
{
    public Guid PublicId { get; set; }
    public string StadiumName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int Capacity { get; set; }
}
