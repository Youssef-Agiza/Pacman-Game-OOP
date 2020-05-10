VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "OauthWrapper"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False


Option Explicit

Public AccessToken As String
Public RefreshToken As String
Public UserEmail As String
Public AuthorizationCode As String
Public ParseAuthorizationCodeInHtml As Boolean
Public ProviderType As Integer
Public RedirectUri As String
Public UseHttpListener As Boolean

Private clientId As String
Private clientSecret As String

Private authUri As String
Private tokenUri As String
Private scope As String
Private prompt As String

Private useClientSecretInRequest As Boolean

Private m_lastError As String
Private m_tokenExpiresInSeconds As Long
Private m_accessTokenTimeStamp As Date

Const GoogleSmtpProvider = 0
Const MsLiveProvider = 1
Const MsOffice365Provider = 2
Const GoogleGmailApiProvider = 3

Private Sub Class_Initialize()
    m_lastError = ""
    UseHttpListener = False
    Reset
End Sub



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

Public Sub InitGoogleSmtpProvider()
    Reset
    
    authUri = "https://accounts.google.com/o/oauth2/v2/auth"
    tokenUri = "https://www.googleapis.com/oauth2/v4/token"
    RedirectUri = "urn:ietf:wg:oauth:2.0:oob"

    clientId = "1072602369179-aru4rj97ateiho9rt4pf5i8l1r01mc16.apps.googleusercontent.com"
    clientSecret = "Lnw8r5FvfKFNS_CSEucbdIE-"
    scope = "email%20profile%20https://mail.google.com"
    prompt = "login"

    useClientSecretInRequest = True
    ParseAuthorizationCodeInHtml = True

    ProviderType = GoogleSmtpProvider
End Sub

Public Sub InitGoogleGmailApiProvider()
    Reset
    
    authUri = "https://accounts.google.com/o/oauth2/v2/auth"
    tokenUri = "https://www.googleapis.com/oauth2/v4/token"
    RedirectUri = "urn:ietf:wg:oauth:2.0:oob"

    clientId = "499737360376-iqv4l8v02085jourush1ughrf4k5d1k6.apps.googleusercontent.com"
    clientSecret = "hzfYFWlL0ZQT82xu1v_P3MTr"
    scope = "email%20profile%20https://www.googleapis.com/auth/gmail.send"
    prompt = "login"

    useClientSecretInRequest = True
    ParseAuthorizationCodeInHtml = True

    ProviderType = GoogleGmailApiProvider
End Sub

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
' *
' * Supported account types: please select Accounts in any organizational directory (Any Azure AD directory - Multitenant) and personal Microsoft accounts (e.g. Skype, Xbox)
' *
' * Advanced settings: please set both "Live SDK Support" and "Treat application as a public client" to "Yes"
' *
' * Above client_id and secret support both "Office365 + EWS" and "Live (hotmail, outlook personal account) + Imap4", Office365 Oauth doesn't support IMAP4, only EWS is supported.
' */

Public Sub InitMsOffice365Provider()
    Reset
    
    authUri = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize"
    tokenUri = "https://login.microsoftonline.com/common/oauth2/v2.0/token"
    RedirectUri = "https://login.microsoftonline.com/common/oauth2/nativeclient"

    clientId = "eccbabb2-3377-4265-85c1-ea2fb515f075"
    clientSecret = "QaR_RR:-5WqTY[nni9pdBr9xVybqrAu4"
    scope = "https://outlook.office.com/EWS.AccessAsUser.All%20offline_access%20email%20openid"
    prompt = "login"

    useClientSecretInRequest = False
    ParseAuthorizationCodeInHtml = False

    ProviderType = MsOffice365Provider
End Sub

Public Sub InitMsLiveProvider()
    Reset

    authUri = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize"
    tokenUri = "https://login.microsoftonline.com/common/oauth2/v2.0/token"
    RedirectUri = "https://login.live.com/oauth20_desktop.srf"

    clientId = "eccbabb2-3377-4265-85c1-ea2fb515f075"
    clientSecret = "QaR_RR:-5WqTY[nni9pdBr9xVybqrAu4"
    scope = "wl.offline_access%20wl.signin%20wl.imap%20wl.emails%20email%20openid"
    prompt = "login"

    useClientSecretInRequest = False
    ParseAuthorizationCodeInHtml = False

    ProviderType = MsLiveProvider
End Sub

Public Function GetFullAuthUri() As String
    Dim fullAuthUri As String
    fullAuthUri = authUri & "?client_id=" & clientId & "&scope=" & scope & "&redirect_uri=" & RedirectUri & "&response_type=code&prompt=" & prompt
    GetFullAuthUri = fullAuthUri
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

Private Sub Reset()
    RefreshToken = ""
    AccessToken = ""
    UserEmail = ""
    AuthorizationCode = ""
    m_tokenExpiresInSeconds = 600
    m_accessTokenTimeStamp = Now
End Sub

Public Function RequestAccessTokenAndUserEmail() As Boolean

    RequestAccessTokenAndUserEmail = False

On Error GoTo ErrorHandle

    Dim httpRequest
    Set httpRequest = CreateObject("MSXML2.ServerXMLHTTP")
    
    Dim requestData As String
    requestData = "code=" & AuthorizationCode
    requestData = requestData & "&client_id=" & clientId

    If useClientSecretInRequest Then
        requestData = requestData & "&client_secret=" & clientSecret
    End If

    requestData = requestData & "&redirect_uri=" & RedirectUri
    requestData = requestData & "&grant_type=authorization_code"
    
    httpRequest.setOption 2, 13056
    httpRequest.Open "POST", tokenUri, True
    httpRequest.setRequestHeader "Content-Type", "application/x-www-form-urlencoded"
    httpRequest.Send requestData
    
    Do While httpRequest.ReadyState <> 4
        DoEvents
        httpRequest.waitForResponse (1)
    Loop
    
    Dim Status
    Status = httpRequest.Status
    
    If Status < 200 Or Status >= 300 Then
        m_lastError = "Failed to get access token from server."
        Reset
        Exit Function
    End If


    Dim result
    result = httpRequest.responseText
    
    Dim oauthParser As New EASendMailObjLib.OAuthResponseParser
    oauthParser.Load result
    
    AccessToken = oauthParser.AccessToken
    RefreshToken = oauthParser.RefreshToken
    UserEmail = oauthParser.EmailInIdToken
    m_tokenExpiresInSeconds = oauthParser.TokenExpiresInSeconds
    
    If AccessToken = "" Then
        m_lastError = "Failed to parse access token from server response."
        Reset
        Exit Function
    End If
    
    If UserEmail = "" Then
         m_lastError = "Failed to parse user email from server response."
         Reset
        Exit Function
    End If
    
    RequestAccessTokenAndUserEmail = True
    m_accessTokenTimeStamp = Now
    Exit Function

ErrorHandle:
    If Err.Description <> "" Then
        m_lastError = Err.Description
    Else
        m_lastError = "Server timeout (access token)."
    End If
    Reset

End Function

Public Function AccessTokenIsExpired() As Boolean
    AccessTokenIsExpired = (DateDiff("s", m_accessTokenTimeStamp, Now) > (m_tokenExpiresInSeconds - 30))
End Function

Public Function RefreshAccessToken() As Boolean
    RefreshAccessToken = False
        
    If RefreshToken = "" Then
        m_lastError = "Refresh token is non-existed."
        Reset
        Exit Function
    End If
    
On Error GoTo ErrorHandle

    Dim httpRequest
    Set httpRequest = CreateObject("MSXML2.ServerXMLHTTP")
    
    Dim requestData As String
    requestData = "client_id=" & clientId
    requestData = requestData & "&client_secret=" & clientSecret
    requestData = requestData & "&refresh_token=" & RefreshToken
    requestData = requestData & "&grant_type=refresh_token"

    httpRequest.setOption 2, 13056
    httpRequest.Open "POST", tokenUri, True
    httpRequest.setRequestHeader "Content-Type", "application/x-www-form-urlencoded"
    httpRequest.Send requestData
    
    Do While httpRequest.ReadyState <> 4
        DoEvents
        httpRequest.waitForResponse (1)
    Loop
    
    Dim Status
    Status = httpRequest.Status
    
    If Status < 200 Or Status >= 300 Then
        m_lastError = "Failed to refresh access token from server."
        Reset
        Exit Function
    End If

    Dim result
    result = httpRequest.responseText
    
    Dim oauthParser As New EASendMailObjLib.OAuthResponseParser
    oauthParser.Load result

    AccessToken = oauthParser.AccessToken
    If AccessToken = "" Then
        m_lastError = "Failed to parse access token from refresh response."
        Reset
        Exit Function
    End If
    
    If oauthParser.RefreshToken <> "" Then
        RefreshToken = oauthParser.RefreshToken
    End If
    
    If oauthParser.TokenExpiresInSeconds <> 0 Then
        m_tokenExpiresInSeconds = oauthParser.TokenExpiresInSeconds
    End If
    m_accessTokenTimeStamp = Now
    RefreshAccessToken = True
    Exit Function
    
ErrorHandle:
    m_lastError = "Server timeout (refresh token)."
    Reset

End Function

Public Sub ClearError()
    m_lastError = ""
End Sub

Public Sub Clear()
    m_lastError = ""
    Reset
End Sub

Public Function GetLastError() As String
    GetLastError = m_lastError
End Function


