using System.Security.Claims;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;

using static Duende.IdentityServer.Models.IdentityResources;

namespace IdentityServer;

public class Config
{
    public static IEnumerable<Client> Clients => new Client[]
    {
        new Client
        {
            ClientId = "movies_mvc_client",
            ClientName = "Movies MVC Web App",
            AllowedGrantTypes = GrantTypes.Hybrid,
            RequirePkce = false,
            AllowRememberConsent = false,
            RedirectUris = new List<string>
            {
                "https://localhost:5002/signin-oidc"
            },
            PostLogoutRedirectUris = new List<string>
            {
                "https://localhost:5002/signout-callback-oidc"
            },
            ClientSecrets = new List<Secret>
            {
                new Secret("secret".Sha256())
            },
            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Address,
                IdentityServerConstants.StandardScopes.Email,
                "movieAPI"
            }
        }
    };

    public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
    {
        new ApiScope("movieAPI", "Movie API")
    };

    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {

    };

    public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
    {
        new OpenId(),
        new Profile(),
        new Address(),
        new Email(),
        new IdentityResource(
            "roles",
            "Your role(s)",
            new List<string>() { "role" }
        )
    };

    public static List<TestUser> TestUsers => new()
    {
        new TestUser
        {
            SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
            Username = "username",
            Password = "password",
            Claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.GivenName, "username"),
                new Claim(JwtClaimTypes.FamilyName, "familyname"),
            }
        }
    };
}
