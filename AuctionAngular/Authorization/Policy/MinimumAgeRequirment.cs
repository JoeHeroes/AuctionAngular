using Microsoft.AspNetCore.Authorization;

namespace AuctionAngular.Authorization.Policy
{
    public class MinimumAgeRequirment:IAuthorizationRequirement
    {
        public int MinAge { get;}

        public MinimumAgeRequirment(int minAge)
        {
            MinAge = minAge;
        }
    }
}
