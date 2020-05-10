#pragma once
class OauthWrapper
{

public:
	OauthWrapper();
	~OauthWrapper();

public:
	CString AccessToken;
	CString RefreshToken;
	CString UserEmail;
	CString AuthorizationCode;
	CString RedirectUri;

	bool ParseAuthorizationCodeInHtml;
	bool UseHttpListener;
	int ProviderType;

public:
	void InitGoogleSmtpProvider();
	void InitMsLiveProvider();
	void InitMsOffice365Provider();
	void InitGoogleGmailApiProvider();

public:
	CString GetFullAuthUri();
	BOOL RequestAccessTokenAndUserEmail();
	BOOL AccessTokenIsExpired();
	BOOL RefreshAccessToken();
	void ClearError();
	void Clear();
	CString GetLastError();
	void ResetLocalRedirectUri();

private:
	void Reset();
	void DoEvents();
	BOOL PostHttp(CString &requestData, CString &requestUri, CString &result);

private:
	CString _clientId;
	CString _clientSecret;
	CString _scope; 
	CString _authUri;
	CString _tokenUri;
	CString _prompt;

	bool _useClientSecretInRequest;

private:
	CString m_lastError;
	int m_tokenExpiresInSeconds;
	time_t m_accessTokenTimeStamp;
};

