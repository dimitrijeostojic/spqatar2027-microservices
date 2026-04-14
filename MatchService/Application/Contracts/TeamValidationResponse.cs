using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts;

public sealed class TeamValidationResponse
{
    public bool Exists { get; set; }
    public Guid PublicId { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public Guid? GroupPublicId { get; set; }
}