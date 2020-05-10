
unit OauthWrapper;

interface

uses  Windows, Forms, SysUtils, Variants, Classes, EASendMailObjLib_TLB, DateUtils, MSXML2_TLB;

type
  TOauthWrapper = class
    public
        AccessToken: string;
        RefreshToken: string;
        UserEmail: string;
        AuthorizationCode: string;
        ParseAuthorizationCodeInHtml: Boolean;
        RedirectUri: string;
        UseHttpListener: Boolean;
        
        constructor Create;

        procedure InitGoogleSmtpProvider();
        procedure InitMsOffice365Provider();
        procedure InitMsLiveProvider();
        procedure InitGoogleGmailApiProvider();

        function GetFullAuthUri(): string;
        function RequestAccessTokenAndUserEmail(): Boolean;
        function AccessTokenIsExpired(): Boolean;
        function RefreshAccessToken(): Boolean;
        function GetLastError(): string;
        procedure ClearError();
        procedure Clear();
        procedure ResetLocalRedirectUri();

    private
        m_lastError: string;
        m_tokenExpiresInSeconds: integer;
        m_accessTokenTimeStamp: TDateTime;    

        clientId: string;
        clientSecret: string;

        authUri: string;
        tokenUri: string;

        scope: string;
        prompt: string;

        useClientSecretInRequest: Boolean;

        providerType: integer;

        procedure Reset();
  end;

  const
        GoogleSmtpProvider = 0;
        MsLiveProvider = 1;
        MsOffice365Provider = 2;
        GoogleGmailApiProvider = 3;

  implementation 

constructor TOauthWrapper.Create;
begin
    m_lastError := '';
    providerType := GoogleSmtpProvider;
    UseHttpListener := false;

    Reset();
end;

procedure TOauthWrapper.InitGoogleSmtpProvider();
begin
    Reset();
    
    authUri := 'https://accounts.google.com/o/oauth2/v2/auth';
    tokenUri := 'https://www.googleapis.com/oauth2/v4/token';
    RedirectUri := 'urn:ietf:wg:oauth:2.0:oob';

    clientId := '1072602369179-aru4rj97ateiho9rt4pf5i8l1r01mc16.apps.googleusercontent.com';
    clientSecret := 'Lnw8r5FvfKFNS_CSEucbdIE-';
    scope := 'email%20profile%20https://mail.google.com';
    prompt := 'login';

    useClientSecretInRequest := true;
    ParseAuthorizationCodeInHtml := true;

    providerType := GoogleSmtpProvider;

end;

procedure TOauthWrapper.InitGoogleGmailApiProvider();
begin
    Reset();
    
    authUri := 'https://accounts.google.com/o/oauth2/v2/auth';
    tokenUri := 'https://www.googleapis.com/oauth2/v4/token';
    RedirectUri := 'urn:ietf:wg:oauth:2.0:oob';

    clientId := '499737360376-iqv4l8v02085jourush1ughrf4k5d1k6.apps.googleusercontent.com';
    clientSecret := 'hzfYFWlL0ZQT82xu1v_P3MTr';
    scope := 'email%20profile%20https://www.googleapis.com/auth/gmail.send';
    prompt := 'login';

    useClientSecretInRequest := true;
    ParseAuthorizationCodeInHtml := true;

    providerType := GoogleGmailApiProvider;

end;

procedure TOauthWrapper.InitMsOffice365Provider();
begin
    Reset();
    
    authUri := 'https://login.microsoftonline.com/common/oauth2/v2.0/authorize';
    tokenUri := 'https://login.microsoftonline.com/common/oauth2/v2.0/token';
    RedirectUri := 'https://login.microsoftonline.com/common/oauth2/nativeclient';

    clientId := 'eccbabb2-3377-4265-85c1-ea2fb515f075';
    clientSecret := 'QaR_RR:-5WqTY[nni9pdBr9xVybqrAu4';
    scope := 'https://outlook.office.com/EWS.AccessAsUser.All%20offline_access%20email%20openid';
    prompt := 'login';

    useClientSecretInRequest := false;
    ParseAuthorizationCodeInHtml := false;

    providerType := MsOffice365Provider;

end;

procedure TOauthWrapper.InitMsLiveProvider();
begin
    Reset();
    
    authUri := 'https://login.microsoftonline.com/common/oauth2/v2.0/authorize';
    tokenUri := 'https://login.microsoftonline.com/common/oauth2/v2.0/token';
    RedirectUri := 'https://login.live.com/oauth20_desktop.srf';

    clientId := 'eccbabb2-3377-4265-85c1-ea2fb515f075';
    clientSecret := 'QaR_RR:-5WqTY[nni9pdBr9xVybqrAu4';
    scope := 'wl.offline_access%20wl.signin%20wl.imap%20wl.emails%20email%20openid';
    prompt := 'login';

    useClientSecretInRequest := false;
    ParseAuthorizationCodeInHtml := false;

    providerType := MsLiveProvider;

end;

procedure TOauthWrapper.ResetLocalRedirectUri();
begin
    if providerType = MsLiveProvider then
        RedirectUri := 'https://login.live.com/oauth20_desktop.srf'
    else if providerType = MsOffice365Provider then
        RedirectUri := 'https://login.microsoftonline.com/common/oauth2/nativeclient'
    else
        RedirectUri := 'urn:ietf:wg:oauth:2.0:oob';
end;


procedure TOauthWrapper.Reset();
begin
    RefreshToken := '';
    AccessToken := '';
    UserEmail := '';
    AuthorizationCode := '';
    m_tokenExpiresInSeconds := 600;
    m_accessTokenTimeStamp := Now();
end;

function TOauthWrapper.GetFullAuthUri(): string;
begin
    result := authUri + '?client_id=' + clientId + '&scope=' + scope + '&redirect_uri=' + RedirectUri
    + '&response_type=code&prompt=' + prompt;
end;

function TOauthWrapper.AccessTokenIsExpired(): Boolean;
begin
    result := (IncSecond(m_accessTokenTimeStamp, m_tokenExpiresInSeconds - 30) < Now());
end;

function TOauthWrapper.GetLastError(): string;
begin
    result := m_lastError;
end;

procedure TOauthWrapper.ClearError();
begin
    m_lastError := '';
end;

procedure TOauthWrapper.Clear();
begin
    m_lastError := '';
    Reset();
end;

function TOauthWrapper.RequestAccessTokenAndUserEmail(): Boolean;
var
    httpRequest: TServerXMLHTTP;
    oauthParser: TOAuthResponseParser;
    requestData: OleVariant;
    status: integer;
    responseText: string;
begin
    result := false;

    httpRequest := TServerXMLHTTP.Create(Application);
    
    requestData := 'code=';
    requestData := requestData + AuthorizationCode;
    requestData := requestData + '&client_id=' + clientId;
    
    if useClientSecretInRequest then
      requestData := requestData + '&client_secret=' + clientSecret;

    requestData := requestData + '&redirect_uri=' + RedirectUri;
    requestData := requestData + '&grant_type=authorization_code';

    httpRequest.setOption(2, 13056);
    httpRequest.open('POST', tokenUri, true);
    httpRequest.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    httpRequest.send(requestData);

    while( httpRequest.readyState <> 4 ) do
    begin
      try
        httpRequest.waitForResponse(1);
        Application.ProcessMessages();
      except
        m_lastError := 'Server response timeout (access token).';
        Reset();
        exit;
      end;
    end;

    status := httpRequest.status;
    if (status < 200) or (status >= 300) then
    begin
        m_lastError := 'Failed to get access token from server.';
        Reset();
      exit;
    end;
    
    responseText := httpRequest.responseText;
    oauthParser := TOAuthResponseParser.Create(Application);
    oauthParser.Load(responseText);

    AccessToken := oauthParser.AccessToken;
    RefreshToken := oauthParser.RefreshToken;
    UserEmail := oauthParser.EmailInIdToken;
    m_tokenExpiresInSeconds := oauthParser.TokenExpiresInSeconds;
    
    if AccessToken = '' then
    begin
        m_lastError := 'Failed to parse access token from server response.';
        Reset();
        exit;
    end;
    
    if UserEmail = '' then
    begin
        m_lastError := 'Failed to parse user email from server response.';
        Reset();
        exit;
    end;

    m_accessTokenTimeStamp := Now();    
    result := True
end;

function TOauthWrapper.RefreshAccessToken(): Boolean;
var
    httpRequest: TServerXMLHTTP;
    oauthParser: TOAuthResponseParser;
    requestData: OleVariant;
    status: integer;
    responseText: string;
begin
    result := false;
    
    if RefreshToken = '' then
    begin
        m_lastError := 'Refresh token is non-existed.';
        Reset();
        exit;
    end;

    httpRequest := TServerXMLHTTP.Create(Application);
    
    requestData := 'client_id=';
    requestData := requestData + clientId;
    requestData := requestData + '&client_secret=' + clientSecret;
    requestData := requestData + '&refresh_token=' + RefreshToken;
    requestData := requestData + '&grant_type=refresh_token';

    httpRequest.setOption(2, 13056);
    httpRequest.open('POST', tokenUri, true);
    httpRequest.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    httpRequest.send(requestData);

    while( httpRequest.readyState <> 4 ) do
    begin
      try
        httpRequest.waitForResponse(1);
        Application.ProcessMessages();
      except
        m_lastError := 'Server response timeout (refresh token).';
        Reset();
        exit;
      end;
    end;

    status := httpRequest.status;
    if (status < 200) or (status >= 300) then
    begin
        m_lastError := 'Failed to refresh access token from server.';
        Reset();
      exit;
    end;
    
    responseText := httpRequest.responseText;

    oauthParser := TOAuthResponseParser.Create(Application);
    oauthParser.Load(responseText);
    
    AccessToken := oauthParser.AccessToken;
    if AccessToken = '' then
    begin
        m_lastError := 'Failed to parse access token from refresh response.';
        Reset();
        exit;
    end;
    
    if oauthParser.RefreshToken <> '' then
        RefreshToken := oauthParser.RefreshToken;
    
    if oauthParser.TokenExpiresInSeconds <> 0 then
        m_tokenExpiresInSeconds := oauthParser.TokenExpiresInSeconds;

    m_accessTokenTimeStamp := Now();
    result := true;
end;


end.