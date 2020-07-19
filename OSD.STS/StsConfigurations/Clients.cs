using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSD.STS.StsConfigurations
{
    internal class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>()
            {
                new Client() 
                {
                    ClientId = "BdWebAppClientId",
                    ClientName = "Bd Web App",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = new List<Secret> {
                        new Secret("secret".Sha256())},
                    AllowedScopes = new List<string> 
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "Bd.Web.Api" 
                    },
                    RedirectUris = new List<string>
                    {
                                                //BdWebApp
                        "https://localhost:44386/signout-callback-oidc",
                        //"https://localhost:44386",
                        //"https://192.168.1.25:44386/signout-callback-oidc"

                        //"https://dealonwheels.azurewebsites.net/signout-callback-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                                                //BdWebApp
                        "https://localhost:44386/signout-callback-oidc",
                        //"https://localhost:44386",
                        //"https://192.168.1.25:44386/signout-callback-oidc"

                        //"https://dealonwheels.azurewebsites.net/signout-callback-oidc"
                    }
                }
            };
        }
    }
}
