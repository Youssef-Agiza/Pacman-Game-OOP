using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EASendMail.Oauth
{
    public class OauthProvider
    {
        public const int GoogleProvider = 0;
        public const int MsLiveProvider = 1;
        public const int MsOffice365Provider = 2;
        /*
         Do not use our test client_id, client_secret in your production environment, you should create your client_id/client_secret for your application.
         */

        /*
         To use Google OAUTH in your application, you must create a project in Google Developers Console.

        - Create your project at https://console.developers.google.com/project.
        - Select your project -> APIs & Services -> Dashboard -> Credentials;
        - Credentials -> Create Credentials -> OAuth client ID -> Web application or Other (Desktop Application). 
          It depends on your application type.

        - Input a name for your application, input your current ASP/ASP.NET URL at Authorized redirect URIs, 
          for example: http://localhost/gmailoauth/default.aspx. (Desktop Application doesn't require this step)
          Click "Create", you will get your client id and client secret.

        - Finally you can also set detail information for your project at Credentials -> OAuth consent screen.
    
        - If you used https://mail.google.com , email, profile scope, you should verify your application that is inroduced in cosent screen.
          If you don't verify your application, your application is limited by some conditions.

          You must apply for your client id and client secret, don't use the client id in the sample project, because it is limited now.
          If you got "This app isn't verified" information, please click "advanced" -> Go to ... for test.
        */
        public static OauthProvider CreateGoogleProvider(string clientId, string clientSecret)
        {
            OauthProvider provider = new OauthProvider();
            provider.AuthUri = "https://accounts.google.com/o/oauth2/v2/auth";
            provider.TokenUri = "https://www.googleapis.com/oauth2/v4/token";
            provider.RedirectUri = "urn:ietf:wg:oauth:2.0:oob";

            provider.ClientId = clientId;
            provider.ClientSecret = clientSecret;
            provider.Scope = "email%20profile%20https://mail.google.com";
            provider.Prompt = "login";

            provider.UseClientSecretInRequest = true;
            provider.ParseAuthorizationCodeInHtml = true;

            return provider;
        }

        public static OauthProvider CreateGoogleProvider()
        {
            return CreateGoogleProvider("1072602369179-aru4rj97ateiho9rt4pf5i8l1r01mc16.apps.googleusercontent.com",
                                "Lnw8r5FvfKFNS_CSEucbdIE-");
        }

        /*
         To use Microsoft OAUTH in your application, you must create a project in https://portal.azure.com.

        - Sign in to the Azure portal using either a work or school account or a personal Microsoft account.
        - If your account gives you access to more than one tenant, select your account in the top right corner, and set your portal session to the Azure AD tenant that you want.
        - In the left-hand navigation pane, select the Azure Active Directory service, and then select App registrations > New registration.
        
         * When the Register an application page appears, enter your application's registration information:

            - Name - Enter a meaningful application name that will be displayed to users of the app.
            - Supported account types - Select which accounts you would like your application to support. Because we need to support all Office 365 and LIVE SDK (hotmail, outlook personal account)
                   select Accounts "in any organizational directory and personal Microsoft accounts"
        
         * Redirect URI (optional) - Select the type of app you're building, Web or Public client (mobile & desktop), and then enter the redirect URI (or reply URL) for your application.

            For web applications, provide the base URL of your app. For example, http://localhost:31544 might be the URL for a web app running on your local machine. 
              Users would use this URL to sign in to a web client application.
            For public client applications, provide the URI used by Azure AD to return token responses. Enter a value specific to your application, such as myapp://auth.
         * 
         
         - When finished, select Register. 
         * 
         * Azure AD assigns a unique application (client) ID to your app, and you're taken to your application's Overview page. 
         * click "Certificates and secrets" -> "client secrets" and add a new client secret. 
         * Important: Please store "client secret" by yourself, because it is hidden when you view it at next time.
         
         - API  Permission
         * Click "API Permission" -> "Add a permission" -> "Exchange" -> "Delegated Permission" -> "Check EWS.AccessAsUser.All"
         *                        -> "Add a permission" -> "Microsoft Graph" -> "Delegated Permission" -> "User.Read", "email", "offline_access", "openid" and "profile"
         
         - Authentication
         * Click "Authentication" ->  
         *    Implicit grant: check "Access tokens" and "ID tokens"
         *    Redirect URI: input the url to get authorization code, for native desktop application, you don't have to add redirect uri.
         *  
         * Mobile and desktop applications: redirect Uri, please check the following URI.
         *   https://login.microsoftonline.com/common/oauth2/nativeclient
         *   https://login.live.com/oauth20_desktop.srf (LiveSDK)
 
         * Supported account types: please select Accounts in any organizational directory (Any Azure AD directory - Multitenant) and personal Microsoft accounts (e.g. Skype, Xbox)
         * 
         * Advanced settings: please set both "Live SDK Support" and "Treat application as a public client" to "Yes"
         * 
         * Above client_id and secret support both "Office365 + EWS" and "Live (hotmail, outlook personal account) + Imap4", Office365 Oauth doesn't support IMAP4, only EWS is supported.
        */
        public static OauthProvider CreateMsOffice365Provider(string clientId, string clientSecret)
        {
            OauthProvider provider = new OauthProvider();
            provider.AuthUri = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize";
            provider.TokenUri = "https://login.microsoftonline.com/common/oauth2/v2.0/token";
            provider.RedirectUri = "https://login.microsoftonline.com/common/oauth2/nativeclient";

            provider.ClientId = clientId;
            provider.ClientSecret = clientSecret;
            provider.Scope = "https://outlook.office.com/EWS.AccessAsUser.All%20offline_access%20email%20openid";

            provider.UseClientSecretInRequest = false;
            provider.ParseAuthorizationCodeInHtml = false;

            provider.Prompt = "login";

            return provider;
        }

        public static OauthProvider CreateMsOffice365Provider()
        {
            return CreateMsOffice365Provider("eccbabb2-3377-4265-85c1-ea2fb515f075", "QaR_RR:-5WqTY[nni9pdBr9xVybqrAu4");
        }

        public static OauthProvider CreateMsLiveProvider(string clientId, string clientSecret)
        {
            OauthProvider provider = new OauthProvider();
            provider.AuthUri = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize";
            provider.TokenUri = "https://login.microsoftonline.com/common/oauth2/v2.0/token";
            provider.RedirectUri = "https://login.live.com/oauth20_desktop.srf";
            provider.Scope = "wl.offline_access%20wl.signin%20wl.imap%20wl.emails%20email%20openid";

            provider.ClientId = clientId;
            provider.ClientSecret = clientSecret;

            provider.UseClientSecretInRequest = false;
            provider.ParseAuthorizationCodeInHtml = false;

            provider.Prompt = "login";


            return provider;
        }

        public static OauthProvider CreateMsLiveProvider()
        {
            return CreateMsLiveProvider("eccbabb2-3377-4265-85c1-ea2fb515f075", "QaR_RR:-5WqTY[nni9pdBr9xVybqrAu4");
        }

        string _clientId = string.Empty;
        public string ClientId
        {
            get { return _clientId; }
            set { _clientId = value; }
        }

        string _clientSecret = string.Empty;
        public string ClientSecret
        {
            get { return _clientSecret; }
            set { _clientSecret = value; }
        }

        const string LocalRedirectUri = "urn:ietf:wg:oauth:2.0:oob";
        string _redirectUri = LocalRedirectUri;
        public string RedirectUri
        {
            get { return _redirectUri; }
            set { _redirectUri = value; }
        }

        string _scope = "";
        public string Scope
        {
            get { return _scope; }
            set { _scope = value; }
        }

        string _prompt = "";
        public string Prompt
        {
            get { return _prompt; }
            set { _prompt = value; }
        }

        string _accessType = "";
        public string AccessType
        {
            get { return _accessType; }
            set { _accessType = value; }
        }


        string _authUri = "";
        public string AuthUri
        {
            get { return _authUri; }
            set { _authUri = value; }
        }

        string _tokenUri = "";
        public string TokenUri
        {
            get { return _tokenUri; }
            set { _tokenUri = value; }
        }

        string _accessToken = string.Empty;
        public string AccessToken
        {
            get { return _accessToken; }
            set { _accessToken = value; }
        }

        string _userEmail = string.Empty;
        public string UserEmail
        {
            get { return _userEmail; }
            set { _userEmail = value; }
        }

        string _refreshToken = string.Empty;
        public string RefreshToken
        {
            get { return _refreshToken; }
            set { _refreshToken = value; }
        }

        int _tokenExpiresInSeconds = 600;
        public int TokenExpiresInSeconds
        {
            get { return _tokenExpiresInSeconds; }
            set { _tokenExpiresInSeconds = value; }
        }

        DateTime _accessTokenTimeStamp = DateTime.Now;
        public DateTime AccessTokenTimeStamp
        {
            get { return _accessTokenTimeStamp; }
            set { _accessTokenTimeStamp = value; }
        }

        bool _useClientSecretInRequest = true;
        public bool UseClientSecretInRequest
        {
            get { return _useClientSecretInRequest; }
            set { _useClientSecretInRequest = value; }
        }

        bool _parseAuthorizationCodeInHtml = false;
        public bool ParseAuthorizationCodeInHtml
        {
            get { return _parseAuthorizationCodeInHtml; }
            set { _parseAuthorizationCodeInHtml = value; }
        }

        public void ClearToken()
        {
            _accessToken = "";
            _refreshToken = "";
            _userEmail = "";
            _tokenExpiresInSeconds = 600;
        }

        public string GetFullAuthUri()
        {
            string authUri = string.Format("{0}?client_id={1}&scope={2}&redirect_uri={3}&response_type=code",
                    AuthUri, ClientId, Scope, RedirectUri);

            if (Prompt.Length > 0)
            {
                authUri += string.Format("&prompt={0}", Prompt);
            }

            return authUri;
        }

        public string TokenRequestData(string authorizationCode)
        {
            if (UseClientSecretInRequest)
            {
                return string.Format(
                  "code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&grant_type=authorization_code",
                  authorizationCode, ClientId, ClientSecret, RedirectUri);
            }
            else
            {
                return string.Format(
                      "code={0}&client_id={1}&redirect_uri={2}&grant_type=authorization_code",
                      authorizationCode, ClientId, RedirectUri);
            }
        }

        public string RefreshTokenRequestData()
        {
            return string.Format(
                "client_id={0}&client_secret={1}&refresh_token={2}&grant_type=refresh_token",
                ClientId, ClientSecret, RefreshToken);
        }
    }
}
