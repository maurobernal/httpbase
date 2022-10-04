using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace HttpBase.Filters;

public class ClaimsTransformation : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        ClaimsIdentity claimsIdentity = new ClaimsIdentity();
        var role = principal.Claims.Where(w => w.Type == "role").Select(c => c.Value).FirstOrDefault();
        if (role != null)
        {
            role = role.Replace("[", string.Empty);
            role = role.Replace("]", string.Empty);
            role = role.Replace("\"", string.Empty);
        }

        role?.Split(",").ToList().ForEach(f => claimsIdentity.AddClaim(new Claim("role-" + f, f)));
        principal.AddIdentity(claimsIdentity);

        return Task.FromResult(principal);
    }
}
