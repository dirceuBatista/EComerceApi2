using System.Security.Claims;
using LivrariaApi.Models;

namespace LivrariaApi.Extensions;

public static class RoleClaimsExtension
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {
        var result = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email),
        };
        result.AddRange(user.Roles.Select(Role=>new Claim(ClaimTypes.Role,Role.Slug)));
        return result;
    }
}