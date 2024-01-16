using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using HospitalAPI.Provider;

[assembly: OwinStartup(typeof(HospitalAPI.BLStartup))]

namespace HospitalAPI
{
    /// <summary>
    /// For authorizing with tokens
    /// </summary>
    public class BLStartup
    {
        #region Public Methods

        /// <summary>
        /// For generating tokens
        /// </summary>
        /// <param name="app">Owin startup class</param>
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"), //https://localhost:port/token
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new AppAuthorizationServerProvider()
            };

            app.UseOAuthAuthorizationServer(options);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

        }

        #endregion
    }
}
