using Application.Common;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace StadiumService.Extensions;

public static class ResultExtension
{
    public static IActionResult ToActionResult<T>(this Result<T> result) where T : class
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Value);
        }

        if (result.Error == ApplicationErrors.NotFound)
        {
            return new NotFoundObjectResult(result.Error);
        }

        return new BadRequestObjectResult(result.Error);
    }
}
