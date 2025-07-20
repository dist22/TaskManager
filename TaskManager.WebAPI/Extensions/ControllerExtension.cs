using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.WebAPI.Extensions;

public static class ControllerExtension
{
    public static int GetUserId(this ControllerBase controller)
    {
        var userClaimId = controller.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaimId == null)
            throw new UnauthorizedAccessException();
        return Int32.Parse(userClaimId.Value);
    }
}