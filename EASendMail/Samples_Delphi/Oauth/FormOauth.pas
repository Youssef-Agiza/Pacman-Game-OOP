unit FormOauth;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, OleCtrls, SHDocVw, MSHTML, OauthWrapper, EASendMailObjLib_TLB;


// Because Web Browser control is used for OAUTH,
// Web browser control uses IE7 rendering mode by default,
// it doesn't support latest Google Web Login Page.

// You should install IE 10/IE11 (recommended) or later version on your current machine,
// and then add/mergin the following registry values to use IE 10 mode.

// "Project1.exe" is your executable file name.
// In current project, it is "Project1.exe"
// If you debug it in VS, please also add "Project1.vshost.exe"

// Windows Registry Editor Version 5.00
// [HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION]
//"Project1.exe"=dword:00002AF9
//"Project1.vshost.exe"=dword:00002AF9

//[HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION]
//"Project1.exe"=dword:00002AF9
//"Project1.vshost.exe"=dword:00002AF9

//Appendix - Web Browser Control Mode:

//11001 (0x2AF9) Internet Explorer 11. Webpages are displayed in IE11 Standards mode, regardless of the !DOCTYPE directive.
//11000 (0x2AF8) Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 mode.
//10001 (0x2711) Internet Explorer 10. Webpages are displayed in IE10 Standards mode, regardless of the !DOCTYPE directive.
//10000 (0x2710) Internet Explorer 10. Webpages containing standards-based !DOCTYPE directives are displayed in IE10 mode.
//9999 (0x270F) Internet Explorer 9. Webpages are displayed in IE9 Standards mode, regardless of the !DOCTYPE directive.
//9000 (0x2328) Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.
//8888 (0x22B8) Webpages are displayed in IE8 Standards mode, regardless of the !DOCTYPE directive.
//8000 (0x1F40) Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode.
//7000 (0x1B58) Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode. This mode is kind of pointless since it's the default.

type
  TFormOauth = class(TForm)
    OauthBrowser: TWebBrowser;
    procedure OauthBrowserDocumentComplete(Sender: TObject;
      const pDisp: IDispatch; var URL: OleVariant);
    procedure FormShow(Sender: TObject);
    procedure OauthBrowserNavigateComplete2(Sender: TObject;
      const pDisp: IDispatch; var URL: OleVariant);
    procedure OauthBrowserTitleChange(Sender: TObject;
      const Text: WideString);
    procedure FormCloseQuery(Sender: TObject; var CanClose: Boolean);

  private
     // HttpListener event handler
    procedure OnRequest(ASender: TObject; const oSender: IDispatch; const URL: WideString);
    procedure OnError(ASender: TObject; const oSender: IDispatch; const ErrorDescription: WideString);
    function ParseParameter(URL: string; ParameterName: string):string;

  public
    Oauth: TOauthWrapper;

  end;

var
  FormOauth1: TFormOauth;
  oHttpListener: THttpListener;
  FireErrorEvent: Boolean;

implementation

{$R *.dfm}

procedure TFormOauth.FormShow(Sender: TObject);
var
  authURI: string;
begin
  OauthBrowser.Silent := true;
  FireErrorEvent := true;

  if not Oauth.UseHttpListener then
    begin
      Oauth.ResetLocalRedirectUri();
      authURI := Oauth.GetFullAuthUri();
      OauthBrowser.Navigate(authURI);
      exit;
    end;

  // Http Listener is Google recommended solution for desktop app, 
  // and MS OAUTH supports it as well, but you need to add http://127.0.0.1 to 
  // Azure portal -> Your app -> Authentication -> Mobile and desktop applications: redirect Uri, please check the following URI.
  oHttpListener := THttpListener.Create(self);
  If not oHttpListener.Create1('127.0.0.1', 0) or
      not oHttpListener.BeginGetRequestUrl() Then
      begin
        ShowMessage(oHttpListener.GetLastError());
        self.Destroy();
      end;
  
  oHttpListener.OnRequest := OnRequest;
  oHttpListener.OnError := OnError;

  Oauth.RedirectUri := Format('http://127.0.0.1:%d', [oHttpListener.ListenPort]);
  
  authURI := Oauth.GetFullAuthUri();
  OauthBrowser.Navigate(authURI);
   
end;

procedure TFormOauth.OauthBrowserDocumentComplete(Sender: TObject;
  const pDisp: IDispatch; var URL: OleVariant);
var
  htmlDoc: HTMLDocument;
  htmlInput: IHtmlInputElement;
  code: string;
begin
  if not Oauth.ParseAuthorizationCodeInHtml then
    exit;

  htmlDoc := OauthBrowser.Document as HTMLDocument;
  htmlInput := htmlDoc.getElementById('code') as IHtmlInputElement;
  if htmlInput = nil then
    exit;

  code := htmlInput.value;
  if code <> '' then
  begin
    Oauth.AuthorizationCode := code;
    ModalResult := mrOK;
  end;

end;

function TFormOauth.ParseParameter(URL: string; ParameterName: string):string;
var
  query, parameter, code: string;
  i, mypos, parameterNameLength: integer;
  list: TStrings;
begin
    result := '';
    parameterNameLength := Length(ParameterName);
    query := URL;
    mypos := pos('?', query);

    if(mypos > 0) then
      query := Copy(query, mypos + 1, length(query) - mypos);

    list := TStringList.Create;
    ExtractStrings(['&'], [], PChar(query), list);
    for i:= 0 to list.Count - 1 do
      begin
        parameter := list[i];
        if (length(parameter) > parameterNameLength) and
            (LowerCase(Copy(parameter, 1, parameterNameLength)) = ParameterName) then
          begin
            code := Copy(parameter, parameterNameLength + 1, length(parameter) - parameterNameLength);
            result := code;
            exit;
          end
      end;

end;

procedure TFormOauth.OauthBrowserNavigateComplete2(Sender: TObject;
  const pDisp: IDispatch; var URL: OleVariant);
var
  code: string;
begin
    code := ParseParameter(URL, 'code=');
    if code = '' then
      code := ParseParameter(URL, 'approvalcode=');

    if code <> '' then
      begin
        Oauth.AuthorizationCode := code;
        ModalResult := mrOK;
      end;
end;

procedure TFormOauth.OauthBrowserTitleChange(Sender: TObject;
  const Text: WideString);
begin
   Caption := Text;
end;

procedure TFormOauth.OnRequest(ASender: TObject; const oSender: IDispatch; const URL: WideString);
var
  code, error: string;
begin
  FireErrorEvent := false;

  code := ParseParameter(URL, 'code=');
  if code = '' then
    begin
      oHttpListener.SendResponse('200', 'text/html; charset=utf8', 'Authorization code is not returned, please retry');
      error := ParseParameter(URL, 'error=');
      ShowMessage( 'Error with request url ' + error);
      ModalResult := mrOK;
      exit;
    end;

  oHttpListener.SendResponse('200', 'text/html; charset=utf8', 'Authorization code is returned, please return to your app');
  OauthBrowser.Stop();
  Oauth.AuthorizationCode := code;
                         
  ModalResult := mrOK;
end;

procedure TFormOauth.OnError(ASender: TObject; const oSender: IDispatch; const ErrorDescription: WideString);
begin
  if FireErrorEvent then
    begin
      ShowMessage(ErrorDescription);
      ModalResult := mrOK;
    end;
end;

procedure TFormOauth.FormCloseQuery(Sender: TObject;
  var CanClose: Boolean);
begin
  FireErrorEvent := false;
  if oHttpListener <> nil then
    oHttpListener.Close();
end;

end.
