using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Options;

public sealed class ServiceUrlsOptions
{
    public string TeamService { get; set; }
    public string StadiumService { get; set; }
}
