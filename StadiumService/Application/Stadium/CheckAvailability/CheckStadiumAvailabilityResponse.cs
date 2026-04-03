using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stadium.CheckAvailability;

public sealed class CheckStadiumAvailabilityResponse
{
    public bool Exists { get; set; }
    public Guid PublicId { get; set; }
    public string StadiumName { get; set; } = string.Empty;
}
