using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Entities;
using WebApi.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly Role[] _roles;

    public AuthorizeAttribute(params Role[] roles)
    {
        _roles = roles ?? new Role[] { };
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var account = (Account)context.HttpContext.Items["Account"];
        if (account == null || (_roles.Any() && !isAuthorize(account.Role)))
        {
            // not logged in or role not authorized
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }

    private bool isAuthorize(Role role)
    {
        if (_roles.Contains(role))
        {
            return true;
        }
        return false;
    }
}