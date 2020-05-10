Public Class OauthProvider

    Public Const GoogleSmtpProvider As Integer = 0
    Public Const MsLiveProvider As Integer = 1
    Public Const MsOffice365Provider As Integer = 2
    Public Const GoogleGmailApiProvider As Integer = 3

    ' Do not use our test client_id, client_secret in your production environment, you should create your client_id/client_secret for your application.

    ' To use Google OAUTH in your application, you must create a project in Google Developers Console.

    ' - Create your project at https://console.developers.google.com/project.
    ' - Select your project -> APIs & Services -> Dashboard -> Credentials;
    ' - Set detail information for your project at OAuth consent screen.
    ' - Credentials -> Create Credentials -> OAuth client ID -> Web application or Other (Desktop Application). 
    ' It depends on your application type.

    ' - Input a name for your application, input your current ASP/ASP.NET URL at Authorized redirect URIs, 
    ' for example: http://localhost/gmailoauth/default.aspx. (Desktop Application doesn't require this step)
    ' Click "Create", you will get your client id and client secret.

    ' - Enable Gmail API at "Library" -> Search "Gmail" and Enable Gmail API.  
    ' - Go to OAuth consent screen -> Edit App -> Google Api Scopes, 
    '   add "https://mail.google.com/" scope (SMTP protocol)
    '   or "https://www.googleapis.com/auth/gmail.send" scope (Gmail RESTFul API protocol)

    ' - If you used https://mail.google.com or https://www.googleapis.com/auth/gmail.send, email, profile scope, you should verify your application that is inroduced in cosent screen.
    ' If you don't verify your application, your application is limited by some conditions.

    ' You must apply for your client id and client secret, don't use the client id in the sample project, because it is limited now.
    ' If you got "This app isn't verified" information, please click "advanced" -> Go to ... for test.
    ' 
    Public Shared Function CreateGoogleSmtpProvider(ByVal clientId As String, ByVal clientSecret As String) As OauthProvider
        Dim provider As OauthProvider = New OauthProvider()
        provider.AuthUri = "https://accounts.google.com/o/oauth2/v2/auth"
        provider.TokenUri = "https://www.googleapis.com/oauth2/v4/token"
        provider.RedirectUri = "urn:ietf:wg:oauth:2.0:oob"
        provider.Scope = "email%20profile%20https://mail.google.com"
        provider.Prompt = "login"

        provider.ClientId = clientId
        provider.ClientSecret = clientSecret
        
        provider.UseClientSecretInRequest = True
        provider.ParseAuthorizationCodeInHtml = True

        provider.ProviderType = GoogleSmtpProvider

        Return provider
    End Function

    Public Shared Function CreateGoogleSmtpProvider() As OauthProvider
        Return CreateGoogleSmtpProvider("1072602369179-aru4rj97ateiho9rt4pf5i8l1r01mc16.apps.googleusercontent.com", "Lnw8r5FvfKFNS_CSEucbdIE-")
    End Function

    Public Shared Function CreateGoogleGmailApiProvider(ByVal clientId As String, ByVal clientSecret As String) As OauthProvider
        Dim provider As OauthProvider = New OauthProvider()
        provider.AuthUri = "https://accounts.google.com/o/oauth2/v2/auth"
        provider.TokenUri = "https://www.googleapis.com/oauth2/v4/token"
        provider.RedirectUri = "urn:ietf:wg:oauth:2.0:oob"
        provider.Scope = "email%20profile%20https://www.googleapis.com/auth/gmail.send"
        provider.Prompt = "login"

        provider.ClientId = clientId
        provider.ClientSecret = clientSecret

        provider.UseClientSecretInRequest = True
        provider.ParseAuthorizationCodeInHtml = True

        provider.ProviderType = GoogleGmailApiProvider

        Return provider
    End Function

    Public Shared Function CreateGoogleGmailApiProvider() As OauthProvider
        Return CreateGoogleGmailApiProvider("499737360376-iqv4l8v02085jourush1ughrf4k5d1k6.apps.googleusercontent.com","hzfYFWlL0ZQT82xu1v_P3MTr")
    End Function
    ' To use Microsoft OAUTH in your application, you must create a project in https://portal.azure.com.

    ' - Sign in to the Azure portal using either a work or school account or a personal Microsoft account.
    ' - If your account gives you access to more than one tenant, select your account in the top right corner, and set your portal session to the Azure AD tenant that you want.
    ' - In the left-hand navigation pane, select the Azure Active Directory service, and then select App registrations > New registration.

    ' * When the Register an application page appears, enter your application's registration information:

    ' - Name - Enter a meaningful application name that will be displayed to users of the app.
    ' - Supported account types - Select which accounts you would like your application to support. Because we need to support all Office 365 and LIVE SDK (hotmail, outlook personal account)
    ' select Accounts "in any organizational directory and personal Microsoft accounts"

    ' * Redirect URI (optional) - Select the type of app you're building, Web or Public client (mobile & desktop), and then enter the redirect URI (or reply URL) for your application.

    ' For web applications, provide the base URL of your app. For example, http://localhost:31544 might be the URL for a web app running on your local machine. 
    ' Users would use this URL to sign in to a web client application.
    ' For public client applications, provide the URI used by Azure AD to return token responses. Enter a value specific to your application, such as myapp://auth.
    ' * 

    ' - When finished, select Register. 
    ' * 
    ' * Azure AD assigns a unique application (client) ID to your app, and you're taken to your application's Overview page. 
    ' * click "Certificates and secrets" -> "client secrets" and add a new client secret. 
    ' * Important: Please store "client secret" by yourself, because it is hidden when you view it at next time.

    ' - API  Permission
    ' * Click "API Permission" -> "Add a permission" -> "Exchange" -> "Delegated Permission" -> "Check EWS.AccessAsUser.All"
    ' *                        -> "Add a permission" -> "Microsoft Graph" -> "Delegated Permission" -> "User.Read", "email", "offline_access", "openid" and "profile"

    ' - Authentication
    ' * Click "Authentication" ->  
    ' *    Implicit grant: check "Access tokens" and "ID tokens"
    ' *    Redirect URI: input the url to get authorization code, for native desktop application, you don't have to add redirect uri.
    ' *  
    ' * Mobile and desktop applications: redirect Uri, please check the following URI.
    ' *   https://login.microsoftonline.com/common/oauth2/nativeclient
    ' *   https://login.live.com/oauth20_desktop.srf (LiveSDK)
    ' *   http://127.0.0.1 (local Http Listener)

    ' * Supported account types: please select Accounts in any organizational directory (Any Azure AD directory - Multitenant) and personal Microsoft accounts (e.g. Skype, Xbox)
    ' * 
    ' * Advanced settings: please set both "Live SDK Support" and "Treat application as a public client" to "Yes"
    ' * 
    ' * Above client_id and secret support both "Office365 + EWS" and "Live (hotmail, outlook personal account) + Imap4", Office365 Oauth doesn't support IMAP4, only EWS is supported.
    ' */
    Public Shared Function CreateMsOffice365Provider(ByVal clientId As String, ByVal clientSecret As String) As OauthProvider
        Dim provider As OauthProvider = New OauthProvider()
        provider.AuthUri = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize"
        provider.TokenUri = "https://login.microsoftonline.com/common/oauth2/v2.0/token"
        provider.RedirectUri = "https://login.microsoftonline.com/common/oauth2/nativeclient"
        provider.Scope = "https://outlook.office.com/EWS.AccessAsUser.All%20offline_access%20email%20openid"
        provider.Prompt = "login"

        provider.ClientId = clientId
        provider.ClientSecret = clientSecret
        
        provider.UseClientSecretInRequest = False
        provider.ParseAuthorizationCodeInHtml = False

        provider.ProviderType = MsOffice365Provider

        Return provider
    End Function

    Public Shared Function CreateMsOffice365Provider() As OauthProvider
        Return CreateMsOffice365Provider("eccbabb2-3377-4265-85c1-ea2fb515f075", "QaR_RR:-5WqTY[nni9pdBr9xVybqrAu4")
    End Function

    Public Shared Function CreateMsLiveProvider(ByVal clientId As String, ByVal clientSecret As String) As OauthProvider
        Dim provider As OauthProvider = New OauthProvider()
        provider.AuthUri = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize"
        provider.TokenUri = "https://login.microsoftonline.com/common/oauth2/v2.0/token"
        provider.RedirectUri = "https://login.live.com/oauth20_desktop.srf"
        provider.Scope = "wl.offline_access%20wl.signin%20wl.imap%20wl.emails%20email%20openid"
        provider.Prompt = "login"

        provider.ClientId = clientId
        provider.ClientSecret = clientSecret

        provider.UseClientSecretInRequest = False
        provider.ParseAuthorizationCodeInHtml = False

        provider.ProviderType = MsLiveProvider

        Return provider
    End Function

    Public Shared Function CreateMsLiveProvider() As OauthProvider
        Return CreateMsLiveProvider("eccbabb2-3377-4265-85c1-ea2fb515f075", "QaR_RR:-5WqTY[nni9pdBr9xVybqrAu4")
    End Function

    Public Sub ResetLocalRedirectUri()
        Select Case ProviderType
            Case MsLiveProvider
                RedirectUri = "https://login.live.com/oauth20_desktop.srf"
            Case MsOffice365Provider
                RedirectUri = "https://login.microsoftonline.com/common/oauth2/nativeclient"
            Case Else
                RedirectUri = "urn:ietf:wg:oauth:2.0:oob"
        End Select
    End Sub

    Private _useHttpListener As Boolean = False
    Public Property UseHttpListener As Boolean
        Get
            Return _useHttpListener
        End Get
        Set(value As Boolean)
            _useHttpListener = value
        End Set
    End Property

    Private _providerType As Integer = GoogleSmtpProvider
    Public Property ProviderType As Integer
        Get
            Return _providerType
        End Get
        Private Set(value As Integer)
            _providerType = value
        End Set
    End Property


    Private _clientId As String = String.Empty

    Public Property ClientId As String
        Get
            Return _clientId
        End Get
        Set(ByVal value As String)
            _clientId = value
        End Set
    End Property

    Private _clientSecret As String = String.Empty

    Public Property ClientSecret As String
        Get
            Return _clientSecret
        End Get
        Set(ByVal value As String)
            _clientSecret = value
        End Set
    End Property

    Const LocalRedirectUri As String = "urn:ietf:wg:oauth:2.0:oob"
    Private _redirectUri As String = LocalRedirectUri

    Public Property RedirectUri As String
        Get
            Return _redirectUri
        End Get
        Set(ByVal value As String)
            _redirectUri = value
        End Set
    End Property

    Private _scope As String = ""
    Public Property Scope As String
        Get
            Return _scope
        End Get
        Set(ByVal value As String)
            _scope = value
        End Set
    End Property

    Private _prompt As String = ""
    Public Property Prompt As String
        Get
            Return _prompt
        End Get
        Set(ByVal value As String)
            _prompt = value
        End Set
    End Property

    Private _accessType As String = ""
    Public Property AccessType As String
        Get
            Return _accessType
        End Get
        Set(ByVal value As String)
            _accessType = value
        End Set
    End Property

    Private _authUri As String = ""
    Public Property AuthUri As String
        Get
            Return _authUri
        End Get
        Set(ByVal value As String)
            _authUri = value
        End Set
    End Property

    Private _tokenUri As String = ""
    Public Property TokenUri As String
        Get
            Return _tokenUri
        End Get
        Set(ByVal value As String)
            _tokenUri = value
        End Set
    End Property

    Private _accessToken As String = String.Empty
    Public Property AccessToken As String
        Get
            Return _accessToken
        End Get
        Set(ByVal value As String)
            _accessToken = value
        End Set
    End Property

    Private _userEmail As String = String.Empty

    Public Property UserEmail As String
        Get
            Return _userEmail
        End Get
        Set(ByVal value As String)
            _userEmail = value
        End Set
    End Property

    Private _refreshToken As String = String.Empty

    Public Property RefreshToken As String
        Get
            Return _refreshToken
        End Get
        Set(ByVal value As String)
            _refreshToken = value
        End Set
    End Property

    Private _tokenExpiresInSeconds As Integer = 600

    Public Property TokenExpiresInSeconds As Integer
        Get
            Return _tokenExpiresInSeconds
        End Get
        Set(ByVal value As Integer)
            _tokenExpiresInSeconds = value
        End Set
    End Property

    Private _accessTokenTimeStamp As DateTime = DateTime.Now

    Public Property AccessTokenTimeStamp As DateTime
        Get
            Return _accessTokenTimeStamp
        End Get
        Set(ByVal value As DateTime)
            _accessTokenTimeStamp = value
        End Set
    End Property

    Private _useClientSecretInRequest As Boolean = True

    Public Property UseClientSecretInRequest As Boolean
        Get
            Return _useClientSecretInRequest
        End Get
        Set(ByVal value As Boolean)
            _useClientSecretInRequest = value
        End Set
    End Property

    Private _parseAuthorizationCodeInHtml As Boolean = False

    Public Property ParseAuthorizationCodeInHtml As Boolean
        Get
            Return _parseAuthorizationCodeInHtml
        End Get
        Set(ByVal value As Boolean)
            _parseAuthorizationCodeInHtml = value
        End Set
    End Property

    Public Sub ClearToken()
        _accessToken = ""
        _refreshToken = ""
        _userEmail = ""
        _tokenExpiresInSeconds = 600
    End Sub

    Public Function GetFullAuthUri() As String
        Dim uri As String = String.Format("{0}?client_id={1}&scope={2}&redirect_uri={3}&response_type=code", AuthUri, ClientId, Scope, RedirectUri)

        If Prompt.Length > 0 Then
            uri += String.Format("&prompt={0}", Prompt)
        End If

        Return uri
    End Function

    Public Function TokenRequestData(ByVal authorizationCode As String) As String
        If UseClientSecretInRequest Then
            Return String.Format("code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&grant_type=authorization_code", authorizationCode, ClientId, ClientSecret, RedirectUri)
        Else
            Return String.Format("code={0}&client_id={1}&redirect_uri={2}&grant_type=authorization_code", authorizationCode, ClientId, RedirectUri)
        End If
    End Function

    Public Function RefreshTokenRequestData() As String
        Return String.Format("client_id={0}&client_secret={1}&refresh_token={2}&grant_type=refresh_token", ClientId, ClientSecret, RefreshToken)
    End Function
End Class
