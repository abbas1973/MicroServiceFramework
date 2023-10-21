using Microsoft.AspNetCore.Authorization;

namespace Base.Application.Common.IdentityConfigs.Handlers
{
    public class ClientClaimRequirement : IAuthorizationRequirement
    {
        public ClientClaimRequirement(string policyName, List<string> allowedClaims)
        {
            PolicyName = policyName;
            AllowedClaims = allowedClaims;
        }

        public string PolicyName { get; }
        public List<string> AllowedClaims { get; }
    }
}
