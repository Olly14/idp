using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSD.STS.StsConfigurations
{
    internal class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource() {
                Name = "role",
                UserClaims = new List<string> {"role"}
                }
            };
        }


        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource()
                {
                   Name = "Bd.Web.Api",
                   DisplayName = "Bd Web Api",
                   Description = "Bd Web Api",
                   UserClaims = new List<string> {"role"},
                   ApiSecrets = new List<Secret> {new Secret("secret".Sha256())},
                   //Scopes = new List<Scope>()
                   //{
                   //    new Scope("customAPI.read"),
                   //    new Scope("customAPI.write")
                   //}
                }
            };
        }


    }
}
