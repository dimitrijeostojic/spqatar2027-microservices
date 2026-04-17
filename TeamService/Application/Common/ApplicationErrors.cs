using Core;

namespace Application.Common;

public static class ApplicationErrors
{
    public static readonly Error NotFound = new("Application.NotFound", "The requested resource was not found.");
}
