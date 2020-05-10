#include "stdafx.h"
#include "OauthWrapper.h"

#define GoogleSmtpProvider 0L
#define MsLiveProvider 1L
#define MsOffice365Provider 2L
#define GoogleGmailApiProvider 3L

OauthWrapper::OauthWrapper()
{
	UseHttpListener = false;
	Clear();
}

OauthWrapper::~OauthWrapper()
{
}

/*
Do not use our test client_id, client_secret in your production environment, you should create your client_id/client_secret for your application.
*/

/*
	To use Google OAUTH in your application, you must create a project in Google Developers Console.

	- Create your project at https://console.developers.google.com/project.
	- Select your project -> APIs & Services -> Dashboard -> Credentials;
	- Set detail information for your project at OAuth consent screen.
	- Credentials -> Create Credentials -> OAuth client ID -> Web application or Other (Desktop Application).
	It depends on your application type.

	- Input a name for your application, input your current ASP/ASP.NET URL at Authorized redirect URIs,
	for example: http://localhost/gmailoauth/default.aspx. (Desktop Application doesn't require this step)
	Click "Create", you will get your client id and client secret.
	- Enable Gmail API at "Library" -> Search "Gmail" and Enable Gmail API.
	- Go to OAuth consent screen -> Edit App -> Google Api Scopes,
	add "https://mail.google.com/" scope (SMTP protocol)
	or "https://www.googleapis.com/auth/gmail.send" scope (Gmail RESTFul API protocol)
	- If you used https://mail.google.com or https://www.googleapis.com/auth/gmail.send, email, profile scope, you should verify your application that is inroduced in consent screen.
	If you don't verify your application, your application access is throtted.

	You must apply for your client id and client secret, don't use the client id in the sample project, because it is limited now.
	If you got "This app isn't verified" information, please click "advanced" -> Go to ... for test.
*/
void OauthWrapper::InitGoogleSmtpProvider()
{
	_authUri = _T("https://accounts.google.com/o/oauth2/v2/auth");
	_tokenUri = _T("https://www.googleapis.com/oauth2/v4/token");
	RedirectUri = _T("urn:ietf:wg:oauth:2.0:oob");

	_clientId = _T("1072602369179-aru4rj97ateiho9rt4pf5i8l1r01mc16.apps.googleusercontent.com");
	_clientSecret = _T("Lnw8r5FvfKFNS_CSEucbdIE-");
	_scope = _T("email%20profile%20https://mail.google.com");
	_prompt = _T("login");

	_useClientSecretInRequest = true;
	ParseAuthorizationCodeInHtml = true;

	ProviderType = GoogleSmtpProvider;

	Reset();
}

void OauthWrapper::InitGoogleGmailApiProvider()
{
	_authUri = _T("https://accounts.google.com/o/oauth2/v2/auth");
	_tokenUri = _T("https://www.googleapis.com/oauth2/v4/token");
	RedirectUri = _T("urn:ietf:wg:oauth:2.0:oob");

	_clientId = _T("499737360376-iqv4l8v02085jourush1ughrf4k5d1k6.apps.googleusercontent.com");
	_clientSecret = _T("hzfYFWlL0ZQT82xu1v_P3MTr");
	_scope = _T("email%20profile%20https://www.googleapis.com/auth/gmail.send");
	_prompt = _T("login");

	_useClientSecretInRequest = true;
	ParseAuthorizationCodeInHtml = true;

	ProviderType = GoogleGmailApiProvider;

	Reset();
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
*   http://127.0.0.1 (local Http Listener)

* Supported account types: please select Accounts in any organizational directory (Any Azure AD directory - Multitenant) and personal Microsoft accounts (e.g. Skype, Xbox)
*
* Advanced settings: please set both "Live SDK Support" and "Treat application as a public client" to "Yes"
*
* Above client_id and secret support both "Office365 + EWS" and "Live (hotmail, outlook personal account) + Imap4", Office365 Oauth doesn't support IMAP4, only EWS is supported.
*/

void OauthWrapper::InitMsOffice365Provider()
{
	_authUri = _T("https://login.microsoftonline.com/common/oauth2/v2.0/authorize");
	_tokenUri = _T("https://login.microsoftonline.com/common/oauth2/v2.0/token");
	RedirectUri = _T("https://login.microsoftonline.com/common/oauth2/nativeclient");

	_clientId = _T("eccbabb2-3377-4265-85c1-ea2fb515f075");
	_clientSecret = _T("QaR_RR:-5WqTY[nni9pdBr9xVybqrAu4");
	_scope = _T("https://outlook.office.com/EWS.AccessAsUser.All%20offline_access%20email%20openid");
	_prompt = _T("login");

	_useClientSecretInRequest = false;
	ParseAuthorizationCodeInHtml = false;

	ProviderType = MsOffice365Provider;

	Reset();
}

void OauthWrapper::InitMsLiveProvider()
{
	_authUri = _T("https://login.microsoftonline.com/common/oauth2/v2.0/authorize");
	_tokenUri = _T("https://login.microsoftonline.com/common/oauth2/v2.0/token");
	RedirectUri = _T("https://login.live.com/oauth20_desktop.srf");

	_clientId = _T("eccbabb2-3377-4265-85c1-ea2fb515f075");
	_clientSecret = _T("QaR_RR:-5WqTY[nni9pdBr9xVybqrAu4");
	_scope = _T("wl.offline_access%20wl.signin%20wl.imap%20wl.emails%20email%20openid");
	_prompt = _T("login");

	_useClientSecretInRequest = false;
	ParseAuthorizationCodeInHtml = false;

	ProviderType = MsLiveProvider;

	Reset();
}

CString
OauthWrapper::GetFullAuthUri()
{
	CString uri;
	uri.Format(_T("%s?client_id=%s&scope=%s&redirect_uri=%s&response_type=code&prompt=%s"),
		_authUri, _clientId, _scope, RedirectUri, _prompt);
	return uri;
}

void 
OauthWrapper::ResetLocalRedirectUri()
{
	switch (ProviderType)
	{
	case MsOffice365Provider:
		RedirectUri = _T("https://login.microsoftonline.com/common/oauth2/nativeclient");
		break;
	case MsLiveProvider:
		RedirectUri = _T("https://login.live.com/oauth20_desktop.srf");
		break;
	default:
		RedirectUri = _T("urn:ietf:wg:oauth:2.0:oob");
		break;
	}
}

void
OauthWrapper::Reset()
{
	RefreshToken = _T("");
	AccessToken = _T("");
	UserEmail = _T("");
	AuthorizationCode = _T("");

	m_tokenExpiresInSeconds = 600;
	::time(&m_accessTokenTimeStamp);
}

void
OauthWrapper::ClearError()
{
	m_lastError = _T("");
}

void
OauthWrapper::Clear()
{
	m_lastError = _T("");
	Reset();
}

CString
OauthWrapper::GetLastError()
{
	return m_lastError;
}

BOOL
OauthWrapper::AccessTokenIsExpired()
{
	time_t now;
	::time(&now);

	return (difftime(now, m_accessTokenTimeStamp) > (m_tokenExpiresInSeconds - 30));
}

BOOL
OauthWrapper::RequestAccessTokenAndUserEmail()
{
	CString data;
	if (_useClientSecretInRequest)
	{
		data.Format(_T("code=%s&client_id=%s&client_secret=%s&grant_type=authorization_code&redirect_uri=%s"),
			AuthorizationCode, _clientId, _clientSecret, RedirectUri
		);
	}
	else
	{
		data.Format(_T("code=%s&client_id=%s&grant_type=authorization_code&redirect_uri=%s"),
			AuthorizationCode, _clientId, RedirectUri
		);
	}

	CString responseText;
	if (!PostHttp(data, _tokenUri, responseText))
	{
		return FALSE;
	}

	IOAuthResponseParserPtr oauthParser = NULL;
	oauthParser.CreateInstance(__uuidof(EASendMailObjLib::OAuthResponseParser));
	if (oauthParser == NULL)
	{
		m_lastError = _T("Failed to create OAuthResponseParser Object, please make sure you install latest EASendMail on your machine.\r\n");
		return FALSE;
	}

	oauthParser->Load((const TCHAR*)responseText);

	AccessToken = (const TCHAR*)oauthParser->AccessToken;
	RefreshToken = (const TCHAR*)oauthParser->RefreshToken;
	UserEmail = (const TCHAR*)oauthParser->EmailInIdToken;
	m_tokenExpiresInSeconds = oauthParser->TokenExpiresInSeconds;

	if (AccessToken.GetLength() == 0)
	{
		m_lastError = _T("Failed to parse access token from server response.");
		Reset();
		return FALSE;
	}

	if (UserEmail.GetLength() == 0)
	{
		m_lastError = _T("Failed to parse user email from server response.");
		Reset();
		return FALSE;
	}

	::time(&m_accessTokenTimeStamp);
	return TRUE;
}

BOOL
OauthWrapper::RefreshAccessToken()
{
	CString data;
	data.Format(_T("client_id=%s&client_secret=%s&refresh_token=%s&grant_type=refresh_token"),
		 _clientId, _clientSecret, RefreshToken
	);
	
	CString responseText;
	if (!PostHttp(data, _tokenUri, responseText))
	{
		return FALSE;
	}

	IOAuthResponseParserPtr oauthParser = NULL;
	oauthParser.CreateInstance(__uuidof(EASendMailObjLib::OAuthResponseParser));
	if (oauthParser == NULL)
	{
		m_lastError = _T("Failed to create OAuthResponseParser Object, please make sure you install latest EASendMail on your machine.\r\n");
		return FALSE;
	}

	oauthParser->Load((const TCHAR*)responseText);

	AccessToken = (const TCHAR*)oauthParser->AccessToken;
	if (AccessToken.GetLength() == 0)
	{
		m_lastError = _T("Failed to parse access token from server refresh response.");
		Reset();
		return FALSE;
	}

	CString tempValue = (const TCHAR*)oauthParser->RefreshToken;
	if (tempValue.GetLength() > 0)
	{
		RefreshToken = tempValue;
	}

	if (oauthParser->TokenExpiresInSeconds > 0)
	{
		m_tokenExpiresInSeconds = oauthParser->TokenExpiresInSeconds;
	}

	::time(&m_accessTokenTimeStamp);
	return TRUE;
}

BOOL
OauthWrapper::PostHttp(CString &requestData, CString &requestUri, CString &result)
{
	try
	{
		IServerXMLHTTPRequestPtr httpRequest = NULL;
		httpRequest.CreateInstance(__uuidof(MSXML2::ServerXMLHTTP));
		if (httpRequest == NULL)
		{
			m_lastError = _T("Failed to create XML HTTP Object, please make sure you install MSXML 3.0 on your machine.\r\n");
			return FALSE;
		}

		LPSAFEARRAY	psaHunk = NULL;

		LONG cdata = requestData.GetLength();
		psaHunk = ::SafeArrayCreateVectorEx(VT_UI1, 0, cdata, NULL);

		for (LONG k = 0; k < (int)cdata; k++)
		{
			BYTE ch = (BYTE)requestData[k];
			::SafeArrayPutElement(psaHunk, &k, &ch);
		}

		_variant_t requestBuffer;
		requestBuffer.vt = (VT_ARRAY | VT_UI1);
		requestBuffer.parray = psaHunk;

		_variant_t async(true);
		_bstr_t uri((const TCHAR*)requestUri);

		httpRequest->setOption((MSXML2::SERVERXMLHTTP_OPTION)2, 13056);
		httpRequest->open(L"POST", uri, async, vtMissing, vtMissing);
		httpRequest->setRequestHeader(L"Content-Type", L"application/x-www-form-urlencoded");
		httpRequest->send(requestBuffer);

		while (httpRequest->readyState != 4) {
			httpRequest->waitForResponse(1);
			DoEvents();
		}

		long status = 0;
		httpRequest->get_status(&status);
		BSTR bstrResult1 = NULL;
		CString result1 = (const TCHAR*)httpRequest->responseText;

		if (status < 200 || status >= 300)
		{
			m_lastError = _T("Failed to get access token from server\r\n");
			return FALSE;
		}

		BSTR bstrResult = NULL;
		result = (const TCHAR*)httpRequest->responseText;
		return TRUE;
	}
	catch (_com_error &ep)
	{
		_bstr_t bstr = ep.Description();
		m_lastError = _T("Failed to get access token from server\r\n");
		m_lastError += (const TCHAR*)ep.Description();
		return FALSE;
	}
}

void
OauthWrapper::DoEvents()
{
	MSG msg;
	while (PeekMessage(&msg, NULL, 0, 0, PM_REMOVE))
	{
		if (msg.message == WM_QUIT)
		{
			return;
		}

		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
}