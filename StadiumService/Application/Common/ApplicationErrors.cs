using Core;

namespace Application.Common;

public static class ApplicationErrors
{
    public static readonly Error NotFound = new("Auth.NotFound", "The requested resource was not found.");
}
