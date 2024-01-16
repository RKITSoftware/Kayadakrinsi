using System.Security.Claims;
using System.Threading.Tasks;
using HospitalAPI.UserRepository;
using Microsoft.Owin.Security.OAuth;

namespace HospitalAPI.Provider
{
    /// <summary>
    /// Custom service provider for authorization
    /// </summary>
    public class AppAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        #region Public Methods

        /// <summary>
        /// Authenticates user
        /// </summary>
        /// <param name="context">Information of client creditentials</param>
        /// <returns>Weather user is valid or not</returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        /// <summary>
        /// For granting creditentials resources according to role
        /// </summary>
        /// <param name="context">Information used in OAuth resource owner grant</param>
        /// <returns>Creditenials according to user role</returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var user = UserRepo.ValidateUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("Invalid_grant", "Username or password is incorrect");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.R01F02));
            foreach (var role in user.R01F04.Split(','))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.Trim()));
            }

            context.Validated(identity);

        }

        #endregion
    }
}