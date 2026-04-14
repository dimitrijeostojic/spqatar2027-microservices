using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common;

public static class ApplicationErrors
{
    public static readonly Error NotFound =
        new("Match.NotFound", "The requested resource was not found.");
    public static readonly Error TeamNotFound =
        new("Match.TeamNotFound", "One or both teams were not found.");
    public static readonly Error StadiumNotFound =
        new("Match.StadiumNotFound", "Stadium was not found.");
    public static readonly Error DifferentTeams =
        new("Match.DifferentTeams", "Home and away team must be different.");
    public static readonly Error TeamConflict =
        new("Match.TeamConflict", "One of the teams already has a match at that time.");
    public static readonly Error StadiumConflict =
        new("Match.StadiumConflict", "Stadium is already occupied at that time.");
    public static readonly Error SameMatchExists =
        new("Match.SameMatchExists", "A match between these teams already exists in this group.");
}