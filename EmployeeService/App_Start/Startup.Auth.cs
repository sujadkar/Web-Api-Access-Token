using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using EmployeeService.Providers;
using EmployeeService.Models;
using Microsoft.Owin.Security.Facebook;

namespace EmployeeService
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(15),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "490352431303566",
            //    appSecret: "ee6951c0ad8b09ee30babdd5c529ee65");

            var fbOptions = new FacebookAuthenticationOptions()
            {
                AppId = "490352431303566",
                AppSecret = "ee6951c0ad8b09ee30babdd5c529ee65",
                BackchannelHttpHandler = new EmployeeService.Facebook.FacebookBackChannelHandler(),
                UserInformationEndpoint = "https://graph.facebook.com/v2.4/me?fields=id,email"
            };

            fbOptions.Scope.Add("email");
            app.UseFacebookAuthentication(fbOptions);
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "1041843107015-qndjho67tj50iq4t82pjcgorfhvt75h7.apps.googleusercontent.com",
                ClientSecret = "AIpYnlPSUVr9ztw-AmoRrVQV"
            });
        }
    }
}
//fbAppID
//490352431303566
//secretid
//ee6951c0ad8b09ee30babdd5c529ee65
//clienti
//1041843107015-qndjho67tj50iq4t82pjcgorfhvt75h7.apps.googleusercontent.com
//clients
//AIpYnlPSUVr9ztw-AmoRrVQV
//http://localhost:16691
//http://localhost:16691/api/Account/ExternalLogins?returnUrl=%2F&generateState=true
///api/Account/ExternalLogin?provider=Google&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A16691%2F&state=JsmVudujP7MoARl00hJn1G3WGk5aidgCfUEkLPDREEc1