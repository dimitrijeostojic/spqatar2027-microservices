using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common;

public static class ApplicationErrors
{
    public static readonly Error NotFound = new("Auth.NotFound", "The requested resource was not found.");
    public static readonly Error InvalidCredentials = new("Auth.InvalidCredentials", "Invalid email or password.");
    public static readonly Error RegistrationFailed = new("Auth.RegistrationFailed", "User registration failed.");
}
