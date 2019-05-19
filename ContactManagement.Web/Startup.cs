using ContactManagement.Web.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.Owin.Security.MicrosoftAccount;

[assembly: OwinStartup(typeof(ContactManagement.Web.Startup))]

namespace ContactManagement.Web
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions googleAuthOptions { get; private set; }
        public static FacebookAuthenticationOptions facebookAuthOptions { get; private set; }
        public static MicrosoftAccountAuthenticationOptions microsoftAuthOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuthContext, ContactManagement.Web.Migrations.Configuration>());

        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            //Configure Google External Login
            googleAuthOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "6351758590-7qsdm2li2pqc49gm90kjej5k2b8r85tm.apps.googleusercontent.com",
                ClientSecret = "gnLIxzNTRnX3PnEfoX-KajH7",
                Provider = new GoogleAuthProvider()
            };
            app.UseGoogleAuthentication(googleAuthOptions);

                        
            //Configure Facebook External Login
            facebookAuthOptions = new FacebookAuthenticationOptions()
            {
                AppId = "399155400935474",
                AppSecret = "cb05d7533e1e13fa56ab6a2cba4ebd32",
                CallbackPath = new PathString("/oauth-redirect/facebook"),
                Provider = new FacebookAuthProvider()
            };
            facebookAuthOptions.Scope.Add("afzal_gates@yahoo.com");
            app.UseFacebookAuthentication(facebookAuthOptions);


            //Configure Microsoft External Login
            microsoftAuthOptions = new MicrosoftAccountAuthenticationOptions()
            {
                ClientId = "34c7f0d8-be9c-445e-a393-757ffe489f1c",
                ClientSecret = ".+Yf3aMV@DbwdnBZMa?fzI6hMUeyYa81",
                Provider = new MicrosoftAuthProvider()
            };
            app.UseMicrosoftAccountAuthentication(microsoftAuthOptions);
        }

        
    }

}