//  ===============================================================================
// |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
// |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
// |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
// |    PARTICULAR PURPOSE.                                                    |
// |    Copyright (c)2010 - 2019 ADMINSYSTEM SOFTWARE LIMITED                        |
// |
// |    Project: It demonstrates how to use EASendMailObj to send email with synchronous mode
// |
// |    Author: Ivan Lui (ivan@emailarchitect.net)
//  ===============================================================================
unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, EASendMailObjLib_TLB, StdCtrls, OleCtrls, MSHTML,
  SHDocVw, DateUtils, MSXML2_TLB, FormOauth, OauthWrapper;

type
  TForm1 = class(TForm)
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    textFrom: TEdit;
    textTo: TEdit;
    textCc: TEdit;
    textSubject: TEdit;
    Label5: TLabel;
    GroupBox1: TGroupBox;
    Label6: TLabel;
    textServer: TEdit;
    Label9: TLabel;
    lstCharset: TComboBox;
    Label10: TLabel;
    textAttachment: TEdit;
    btnAdd: TButton;
    btnClear: TButton;
    btnSend: TButton;
    htmlEditor: TWebBrowser;
    lstFont: TComboBox;
    lstSize: TComboBox;
    btnB: TButton;
    btnI: TButton;
    btnU: TButton;
    btnC: TButton;
    btnInsert: TButton;
    ColorDialog1: TColorDialog;
    textStatus: TLabel;
    btnCancel: TButton;
    Label7: TLabel;
    lstPort: TComboBox;
    btnClearToken: TButton;
    Label8: TLabel;
    lstProvider: TComboBox;
    chkUseHttpListener: TCheckBox;
    procedure FormCreate(Sender: TObject);
    procedure btnSendClick(Sender: TObject);
    function ChAnsiToWide(const StrA: AnsiString): WideString;
    procedure btnAddClick(Sender: TObject);
    procedure btnClearClick(Sender: TObject);
    procedure htmlEditorNavigateComplete2(ASender: TObject;
      const pDisp: IDispatch; var URL: OleVariant);
    procedure btnInsertClick(Sender: TObject);
    procedure btnBClick(Sender: TObject);
    procedure btnIClick(Sender: TObject);
    procedure btnUClick(Sender: TObject);
    procedure btnCClick(Sender: TObject);
    procedure lstFontChange(Sender: TObject);
    procedure lstSizeChange(Sender: TObject);
    // EASendMail event handler
    procedure OnAuthenticated(ASender: TObject);
    procedure OnConnected(ASender: TObject);
    procedure OnClosed(ASender: TObject);
    procedure OnError(ASender: TObject;
  lError: Integer; const ErrDescription: WideString);
    procedure OnSending(ASender: TObject; lSent: Integer; lTotal: Integer);
    procedure btnCancelClick(Sender: TObject);
    procedure FormResize(Sender: TObject);
    procedure btnClearTokenClick(Sender: TObject);
    procedure lstProviderChange(Sender: TObject);
    procedure chkUseHttpListenerClick(Sender: TObject);

  private
    procedure InitCharset();
    procedure InitFonts();

    function DoOauth(): Boolean;

  public
    { Public declarations }
  end;

const
  ConnectNormal = 0;
  ConnectSSLAuto = 1;
  ConnectSTARTTLS = 2;
  ConnectDirectSSL = 3;
  ConnectTryTLS = 4;

  CRYPT_MACHINE_KEYSET = 32;
  CRYPT_USER_KEYSET = 4096;
  CERT_SYSTEM_STORE_CURRENT_USER = 65536;
  CERT_SYSTEM_STORE_LOCAL_MACHINE = 131072;
    
var
  Form1: TForm1;
  oSmtp: TMail;

  m_arAttachments : TStringList;
  m_arCharset: array[0..27,0..1] of WideString;

  m_bError: Boolean; //this variable indicates if error occurs when sending email
  m_bCancel: Boolean;
  m_bIdle: Boolean;
  m_lastErrDescription: string;

  Oauth: TOauthWrapper;

implementation

{$R *.dfm}

function TForm1.DoOauth(): Boolean;
var
  OauthDlg: TFormOauth;
begin

  if Oauth.AccessToken <> '' then
  begin
    if not Oauth.AccessTokenIsExpired() then
    begin   
        result := true;
        exit;
    end;
    
    textStatus.Caption := 'Refresh expired access token from server ...';

    If Oauth.RefreshAccessToken() Then
    begin
        result := true;
        exit;
    end;

    Oauth.ClearError();
    textStatus.Caption := 'Failed to refresh expired access token, now request new token again';
  end;

  OauthDlg := TFormOauth.Create(Application);

  Oauth.UseHttpListener := chkUseHttpListener.Checked;
  OauthDlg.Oauth := Oauth;
  OauthDlg.ShowModal();

  If Oauth.AuthorizationCode = '' Then
  begin
      ShowMessage('Failed to login user and get authorization code.');
      result := false;
      exit;
  end;

  textStatus.Caption := 'Request access token from server ...';
  if not Oauth.RequestAccessTokenAndUserEmail() then
  begin      
      ShowMessage(Oauth.GetLastError());
      result := false;
      exit;
  end;

  result := true;
end;

procedure TForm1.OnAuthenticated(ASender: TObject);
begin
  textStatus.Caption := 'Authenticated';
end;

procedure TForm1.OnConnected(ASender: TObject);
begin
  textStatus.Caption := 'Connected';
end;

procedure TForm1.OnClosed(ASender: TObject);
begin
  if not m_bError then
    textStatus.Caption := 'email was sent successfully';

  m_bIdle := true;
end;

procedure TForm1.OnError(ASender: TObject;
  lError: Integer; const ErrDescription: WideString);
begin
  textStatus.Caption := 'Failed to send email with error: ' + ErrDescription;
  m_lastErrDescription := ErrDescription;
  m_bError := true;
  m_bIdle := true;
end;

procedure TForm1.OnSending(ASender: TObject; lSent: Integer; lTotal: Integer);
begin
  textStatus.Caption := Format('Sending %d/%d ...', [lSent, lTotal]);
end;

procedure TForm1.InitCharset();
var
  i, index: integer;
begin
  index := 0;

  m_arCharset[index, 0] := 'Arabic(Windows)';
  m_arCharset[index, 1] := 'windows-1256';
  index := index + 1;

  m_arCharset[index, 0] := 'Baltic(ISO)';
  m_arCharset[index, 1] := 'iso-8859-4';
  index := index + 1;

  m_arCharset[index, 0] := 'Baltic(Windows)';
  m_arCharset[index, 1] := 'windows-1257';
  index := index + 1;

  m_arCharset[index, 0] := 'Central Euporean(ISO)';
  m_arCharset[index, 1] := 'iso-8859-2';
  index := index + 1;

  m_arCharset[index, 0] := 'Central Euporean(Windows)';
  m_arCharset[index, 1] := 'windows-1250';
  index := index + 1;

  m_arCharset[index, 0] := 'Chinese Simplified(GB18030)';
  m_arCharset[index, 1] := 'GB18030';
  index := index + 1;

  m_arCharset[index, 0] := 'Chinese Simplified(GB2312)';
  m_arCharset[index, 1] := 'gb2312';
  index := index + 1;

  m_arCharset[index, 0] := 'Chinese Simplified(HZ)';
  m_arCharset[index, 1] := 'hz-gb-2312';
  index := index + 1;

  m_arCharset[index, 0] := 'Chinese Traditional(Big5)';
  m_arCharset[index, 1] := 'big5';
  index := index + 1;

  m_arCharset[index, 0] := 'Cyrillic(ISO)';
  m_arCharset[index, 1] := 'iso-8859-5';
  index := index + 1;

  m_arCharset[index, 0] := 'Cyrillic(KOI8-R)';
  m_arCharset[index, 1] := 'koi8-r';
  index := index + 1;

  m_arCharset[index, 0] := 'Cyrillic(KOI8-U)';
  m_arCharset[index, 1] := 'koi8-u';
  index := index + 1;

  m_arCharset[index, 0] := 'Cyrillic(Windows)';
  m_arCharset[index, 1] := 'windows-1251';
  index := index + 1;

  m_arCharset[index, 0] := 'Greek(ISO)';
  m_arCharset[index, 1] := 'iso-8859-7';
  index := index + 1;

  m_arCharset[index, 0] := 'Greek(Windows)';
  m_arCharset[index, 1] := 'windows-1253';
  index := index + 1;

  m_arCharset[index, 0] := 'Hebrew(Windows)';
  m_arCharset[index, 1] := 'windows-1255';
  index := index + 1;

  m_arCharset[index, 0] := 'Japanese(JIS)';
  m_arCharset[index, 1] := 'iso-2022-jp';
  index := index + 1;

  m_arCharset[index, 0] := 'Korean';
  m_arCharset[index, 1] := 'ks_c_5601-1987';
  index := index + 1;

  m_arCharset[index, 0] := 'Korean(EUC)';
  m_arCharset[index, 1] := 'euc-kr';
  index := index + 1;

  m_arCharset[index, 0] := 'Latin 9(ISO)';
  m_arCharset[index, 1] := 'iso-8859-15';
  index := index + 1;

  m_arCharset[index, 0] := 'Thai(Windows)';
  m_arCharset[index, 1] := 'windows-874';
  index := index + 1;

  m_arCharset[index, 0] := 'Turkish(ISO)';
  m_arCharset[index, 1] := 'iso-8859-9';
  index := index + 1;

  m_arCharset[index, 0] := 'Turkish(Windows)';
  m_arCharset[index, 1] := 'windows-1254';
  index := index + 1;

  m_arCharset[index, 0] := 'Unicode(UTF-7)';
  m_arCharset[index, 1] := 'utf-7';
  index := index + 1;

  m_arCharset[index, 0] := 'Unicode(UTF-8)';
  m_arCharset[index, 1] := 'utf-8';
  index := index + 1;

  m_arCharset[index, 0] := 'Vietnames(Windows)';
  m_arCharset[index, 1] := 'windows-1258';
  index := index + 1;

  m_arCharset[index, 0] := 'Western European(ISO)';
  m_arCharset[index, 1] := 'iso-8859-1';
  index := index + 1;

  m_arCharset[index, 0] := 'Western European(Windows)';
  m_arCharset[index, 1] := 'windows-1252';


  for i:= 0 to 27 do
  begin
    lstCharset.AddItem(m_arCharset[i,0], nil);
  end;
  // Set default encoding to utf-8, it supports all languages.
  lstCharset.ItemIndex := 24;

  lstPort.AddItem('25', nil);
  lstPort.AddItem('587', nil);
  lstPort.AddItem('465', nil);
  lstPort.ItemIndex := 1;

end;
procedure TForm1.FormCreate(Sender: TObject);
var
  htmlDoc: HTMLDocument;
begin
  
  oSmtp := TMail.Create(Application);
  oSmtp.LicenseCode := 'TryIt';
   
  // Set Asynchronous mode
  oSmtp.Asynchronous := 1;

  // Add event handler
  oSmtp.OnConnected := OnConnected;
  oSmtp.OnClosed := OnClosed;
  oSmtp.OnError := OnError;
  oSmtp.OnSending := OnSending;
  oSmtp.OnAuthenticated := OnAuthenticated;

  textSubject.Text := 'OAUTH Test Sample';

  htmlEditor.Navigate('about:blank');
  htmlDoc := htmlEditor.Document as HTMLDocument;
  htmlDoc.designMode := 'on';
  m_arAttachments := TStringList.Create();
  
  InitCharset();
  InitFonts();

  lstProvider.AddItem('Gmail OAUTH + SMTP', nil);
  lstProvider.AddItem('LIVE OAUTH + SMTP (Hotmail)', nil);
  lstProvider.AddItem('EWS OAUTH + (Office365)', nil);
  lstProvider.AddItem('Gmail API + OAUTH', nil);
  lstProvider.ItemIndex := 0;

  Oauth := TOauthWrapper.Create();
  Oauth.InitGoogleSmtpProvider();

end;

procedure TForm1.btnSendClick(Sender: TObject);
var
  i: integer;
  htmlDoc: HTMLDocument;
  body: HTMLHtmlElement;
  html: WideString;
  ports: Array[0..2] of integer;
begin

  btnSend.Enabled := false;
  if not DoOauth() then
  begin
    textStatus.Caption := 'Failed to request/refresh access token!';
    btnSend.Enabled := true;
    exit;
  end;

  textStatus.Caption := 'Oauth is completed, ready.';
  btnSend.Enabled := true;

  ports[0] := 25;
  ports[1] := 587;
  ports[2] := 465;

  if(trim(textTo.Text) = '') and
     (trim(textCc.Text) = '') then
  begin
    ShowMessage('Please input To or Cc email addresses, please use comma(,) to separate multiple addresses!');
    textTo.SetFocus();
    exit;
  end;


  btnSend.Enabled := False;
  btnCancel.Enabled := True;
  // because m_oSmtp is a member variahle, so we need to clear recipients and attachments
  oSmtp.Reset();
  oSmtp.Asynchronous := 1;

  oSmtp.Charset := m_arCharset[lstCharset.ItemIndex, 1];
  oSmtp.FromAddr := Oauth.UserEmail; //using Gmail address as sender

  if(textFrom.Text <> '') then
    oSmtp.ReplyTo := ChAnsiToWide(trim(textFrom.Text));

  // Add recipient's
  oSmtp.ClearRecipient();
  oSmtp.AddRecipientEx(ChAnsiToWide(trim(textTo.Text)), 0);
  oSmtp.AddRecipientEx(ChAnsiToWide(trim(textCc.Text)), 0);

  // Set subject
  oSmtp.Subject := ChAnsiToWide(textSubject.Text);

  // Using HTML FORMAT to send mail
  oSmtp.BodyFormat := 1;

  // import html body with embedded images
  html := GetCurrentDir();
  htmlDoc := htmlEditor.document as HTMLDocument;
  body := htmlDoc.body as HTMLHtmlElement;
  html := body.innerHTML;

  oSmtp.ImportHtml(html, GetCurrentDir());

  // Add attachments
  for i:= 0 to m_arAttachments.Count - 1 do
  begin
      oSmtp.AddAttachment(ChAnsiToWide(m_arAttachments[i]));
  end;

  oSmtp.ServerAddr := trim(textServer.Text);
  oSmtp.ServerPort := ports[lstPort.ItemIndex];
  oSmtp.Protocol := 0; //SMTP protocol

  if lstProvider.ItemIndex = 2 then
    oSmtp.Protocol := 1; //EWS protoocl

  if lstProvider.ItemIndex = 3 then
    oSmtp.Protocol := 3; //Gmail Api protoocl

  oSmtp.UserName := Oauth.UserEmail;
  oSmtp.Password := Oauth.AccessToken;
  oSmtp.AuthType := 5; // GMAIL XOAUTH2;
  
  // Use SSL/TLS based on server port.
  oSmtp.ConnectType := ConnectSSLAuto;

  btnSend.Enabled := false;
  textStatus.Caption := 'start to send email ...';

  m_bIdle := False;
  m_bCancel := False;
  m_bError := False;
  oSmtp.SendMail();

  while not m_bIdle do
      Application.ProcessMessages();

  if m_bCancel then
    textStatus.Caption := 'Operation is canceled by user.'
  else if m_bError then
    textStatus.Caption := m_lastErrDescription
  else
    textStatus.Caption := 'Message was delivered successfully';

  ShowMessage(textStatus.Caption);
  btnSend.Enabled := True;
  btnCancel.Enabled := False;
  btnClearToken.Enabled := (Oauth.AccessToken <> '');
  htmlEditor.SetFocus();
end;

// before delphi doesn't support unicode very well in VCL, so
// we have to convert the ansistring to unicode by current default codepage.
function TForm1.ChAnsiToWide(const StrA: AnsiString): WideString;
var
  nLen: integer;
begin
  Result := StrA;
  if Result <> '' then
  begin
    // convert ansi string to widestring (unicode) by current system codepage
    nLen := MultiByteToWideChar(GetACP(), 1, PAnsiChar(@StrA[1]), -1, nil, 0);
    SetLength(Result, nLen - 1);
    if nLen > 1 then
      MultiByteToWideChar(GetACP(), 1, PAnsiChar(@StrA[1]), -1, PWideChar(@Result[1]), nLen - 1);
  end;
end;

procedure TForm1.btnAddClick(Sender: TObject);
var
  pFileDlg : TOpenDialog;
  fileName : string;
  index: integer;
begin
  pFileDlg := TOpenDialog.Create(Form1);
  if pFileDlg.Execute() then
  begin
    fileName := pFileDlg.FileName;
    m_arAttachments.Add(fileName);
    while true do
    begin
       index := Pos( '\', fileName);
       if index <= 0 then
          break;

       fileName := Copy(fileName, index+1, Length(fileName)- index );
    end;
    textAttachment.Text := textAttachment.Text + fileName + ';';
  end;
  pFileDlg.Destroy();
  htmlEditor.SetFocus();
end;


procedure TForm1.btnClearClick(Sender: TObject);
begin
  m_arAttachments.Clear();
  textAttachment.Text := '';
  htmlEditor.SetFocus();
end;

procedure TForm1.htmlEditorNavigateComplete2(ASender: TObject;
  const pDisp: IDispatch; var URL: OleVariant);
var
  s: WideString;
  htmlDoc: HTMLDocument;
  body: HTMLHtmlElement;
begin
   s := '<div>This sample demonstrates how to send html email using Gmail/MS Live/MS Office365 OAUTH.</div><div>&nbsp;</div>'
   + '<div>This sample project demonstrates how to send email using Gmail/MS Live/MS Office365 OAUTH, '
   + 'Please create your client_id and client_secret introduced in Form OauthWrapper.</div>'
   + '<div>If you got "This app is not verified" information, please click "advanced" -> Go to ... for test.</div>'
   + '<div>&nbsp;</div>';

   htmlDoc := htmlEditor.Document as HTMLDocument;
   body := htmlDoc.body as HTMLHtmlElement;
   body.innerHTML := s;
end;

procedure TForm1.btnInsertClick(Sender: TObject);
var
  htmlDoc: HTMLDocument;
  parameter: OleVariant;
begin
  htmlEditor.SetFocus();
  htmlDoc := htmlEditor.document as HTMLDocument;
  htmlDoc.execCommand('InsertImage', true, parameter);
  htmlEditor.SetFocus();
end;

procedure TForm1.InitFonts();
var
  arFont: array[0..11] of string;
  i: integer;
begin
  i := 0;
  arFont[i] := 'Choose Font Style ...';
  i := i+1;
  arFont[i] := 'Arial';
  i := i+1;
  arFont[i] := 'Arial Baltic';
  i := i+1;
  arFont[i] := 'Arial Black';
  i := i+1;
  arFont[i] := 'Basemic Symbol';
  i := i+1;
  arFont[i] := 'Bookman Old Style';
  i := i+1;
  arFont[i] := 'Comic Sans MS';
  i := i+1;
  arFont[i] := 'Courier';
  i := i+1;
  arFont[i] := 'Courier New';
  i := i+1;
  arFont[i] := 'Microsoft Sans Serif';
  i := i+1;
  arFont[i] := 'Times New Roman';
  i := i+1;
  arFont[i] := 'Verdana';
  i := i+1;
  
  for i:= 0 to 11 do
    lstFont.AddItem(arFont[i], nil);

  lstFont.ItemIndex := 0;
  lstSize.AddItem('Font Size ...', nil);
  for i:= 1 to 7 do
    lstSize.AddItem(IntToStr(i), nil);

   lstSize.ItemIndex := 0; 
end;
procedure TForm1.btnBClick(Sender: TObject);
var
  htmlDoc: HTMLDocument;
  parameter: OleVariant;
begin
  htmlEditor.SetFocus();
  htmlDoc := htmlEditor.document as HTMLDocument;
  htmlDoc.execCommand('Bold', false, parameter);
  htmlEditor.SetFocus();

end;

procedure TForm1.btnIClick(Sender: TObject);
var
  htmlDoc: HTMLDocument;
  parameter: OleVariant;
begin
  htmlEditor.SetFocus();
  htmlDoc := htmlEditor.document as HTMLDocument;
  htmlDoc.execCommand('Italic', false, parameter);
  htmlEditor.SetFocus();
end;

procedure TForm1.btnUClick(Sender: TObject);
var
  htmlDoc: HTMLDocument;
  parameter: OleVariant;
begin
  htmlEditor.SetFocus();
  htmlDoc := htmlEditor.document as HTMLDocument;
  htmlDoc.execCommand('underline', false, parameter);
  htmlEditor.SetFocus();
end;

procedure TForm1.btnCClick(Sender: TObject);
var
  htmlDoc: HTMLDocument;
  parameter: OleVariant;
  myColor: TColor;
  hexColor: string;
begin
  htmlEditor.SetFocus();
  if not ColorDialog1.Execute() then
    exit;

  myColor := ColorDialog1.Color;

  hexColor := '#'+ IntToHex(GetRValue(myColor), 2) +
     IntToHex(GetGValue(myColor), 2) +
     IntToHex(GetBValue(myColor), 2) ;

  parameter := hexColor;
  htmlDoc := htmlEditor.document as HTMLDocument;
  htmlDoc.execCommand('ForeColor', true, parameter);

  htmlEditor.SetFocus();
end;

procedure TForm1.lstFontChange(Sender: TObject);
var
  htmlDoc: HTMLDocument;
  parameter: OleVariant;
begin
  htmlEditor.SetFocus();
  if lstFont.ItemIndex = 0 then
    exit;

  parameter := lstFont.Text;
  lstFont.ItemIndex := 0;
  htmlDoc := htmlEditor.document as HTMLDocument;
  htmlDoc.execCommand('fontname', false, parameter);
  htmlEditor.SetFocus();

end;

procedure TForm1.lstSizeChange(Sender: TObject);
var
  htmlDoc: HTMLDocument;
  parameter: OleVariant;
begin
  htmlEditor.SetFocus();
  if lstSize.ItemIndex = 0 then
    exit;

  parameter := lstSize.Text;
  lstSize.ItemIndex := 0;
  htmlDoc := htmlEditor.document as HTMLDocument;
  htmlDoc.execCommand('fontsize', false, parameter);
  htmlEditor.SetFocus();

end;

procedure TForm1.btnCancelClick(Sender: TObject);
begin
    oSmtp.Terminate();
    m_bCancel := True;
    m_bIdle := True;
    btnCancel.Enabled := False;
end;

procedure TForm1.FormResize(Sender: TObject);
begin
  if Form1.Width < 670 then
    Form1.Width := 670;

  if Form1.Height < 452 then
    Form1.Height := 452;

  htmlEditor.Width := Form1.Width - 30;
  htmlEditor.Height := Form1.Height - 330;
  btnSend.Top := htmlEditor.Top +  htmlEditor.Height + 5;
  btnSend.Left := Form1.Width - 30 - btnSend.Width - btnCancel.Width - btnClearToken.Width;

  btnCancel.Top := btnSend.Top;
  btnCancel.Left := btnSend.Left + btnSend.Width + 5;

  btnClearToken.Top := btnSend.Top;
  btnClearToken.Left := btnCancel.Left + btnCancel.Width + 5;

  textStatus.Top := btnSend.Top;

  GroupBox1.Left := Form1.Width - GroupBox1.Width - 20;
  textFrom.Width := Form1.Width - GroupBox1.Width - 110;
  textSubject.Width := textFrom.Width;
  textTo.Width := textFrom.Width;
  textCc.Width := textFrom.Width;
end;

procedure TForm1.btnClearTokenClick(Sender: TObject);
begin
  Oauth.Clear();
  btnClearToken.Enabled := false;
end;

procedure TForm1.chkUseHttpListenerClick(Sender: TObject);
begin
  Oauth.Clear();
  btnClearToken.Enabled := false;
end;

procedure TForm1.lstProviderChange(Sender: TObject);
begin
  if lstProvider.ItemIndex = 0 then
    begin
      textServer.Text := 'smtp.gmail.com';
      lstPort.Enabled := true;
      Oauth.InitGoogleSmtpProvider();
    end
  else if lstProvider.ItemIndex = 1 then
    begin
      textServer.Text := 'smtp.live.com';
      lstPort.Enabled := true;
      Oauth.InitMsLiveProvider();
    end
  else if lstProvider.ItemIndex = 2 then
    begin
      textServer.Text := 'outlook.office365.com';
      lstPort.Enabled := false;
      Oauth.InitMsOffice365Provider();
    end
  else if lstProvider.ItemIndex = 3 then
    begin
      textServer.Text := 'https://www.googleapis.com/upload/gmail/v1/users/me/messages/send?uploadType=media';
      lstPort.Enabled := false;
      Oauth.InitGoogleGmailApiProvider();
    end;
end;


end.
