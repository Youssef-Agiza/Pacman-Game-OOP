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
  Dialogs, EASendMailObjLib_TLB, StdCtrls, StrUtils;

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
    chkAuth: TCheckBox;
    Label7: TLabel;
    Label8: TLabel;
    textUser: TEdit;
    textPassword: TEdit;
    chkSSL: TCheckBox;
    chkSign: TCheckBox;
    chkEncrypt: TCheckBox;
    Label9: TLabel;
    lstCharset: TComboBox;
    Label10: TLabel;
    textAttachment: TEdit;
    btnAdd: TButton;
    btnClear: TButton;
    textBody: TMemo;
    btnSend: TButton;
    lstProtocol: TComboBox;
    lstPort: TComboBox;
    procedure FormCreate(Sender: TObject);
    procedure btnSendClick(Sender: TObject);
    procedure chkAuthClick(Sender: TObject);

    procedure btnAddClick(Sender: TObject);
    procedure DirectSend(oSmtp: TMail);
    procedure btnClearClick(Sender: TObject);
    procedure FormResize(Sender: TObject);
    procedure textFromChange(Sender: TObject);
    procedure textServerChange(Sender: TObject);
    procedure textServerKeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure textUserKeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure lstProtocolClick(Sender: TObject);

  private
    procedure InitCharset();

    function ChAnsiToWide(const StrA: AnsiString): WideString; overload;
    function ChAnsiToWide(const StrA: WideString): WideString; overload;

    function GetSenderDomain(const s: string): string;
    function GetSenderAddr(const s: string): string;
    procedure ChangeSettingForWellKnownServer();

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
  m_arAttachments : TStringList;
  m_arCharset: array[0..27,0..1] of WideString;

  m_autoChangeUser: Boolean;
  m_autoChangeServer: Boolean;
  
implementation

{$R *.dfm}

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

  lstProtocol.AddItem('SMTP Protocol - Recommended', nil);
  lstProtocol.AddItem('Exchange Web Service - 2007/2010', nil);
  lstProtocol.AddItem('Exchange WebDav - 2000/2003', nil);
  lstProtocol.ItemIndex := 0;

  lstPort.AddItem('25', nil);
  lstPort.AddItem('587', nil);
  lstPort.AddItem('465', nil);
  lstPort.ItemIndex := 0;

end;
procedure TForm1.FormCreate(Sender: TObject);
begin
  textSubject.Text := 'delphi email test';
  textBody.Text := 'This sample demonstrates how to send simple email.'
  + #13#10 + #13#10
  + 'If no sever address was specified, the email will be delivered to the recipient''s server directly. '
  + 'However, if you don''t have a static IP address, '
  + 'many anti-spam filters will mark it as a junk-email.'
  + #13#10;

  m_arAttachments := TStringList.Create();
  InitCharset();

  m_autoChangeUser := true;
  m_autoChangeServer := true;
end;

procedure TForm1.btnSendClick(Sender: TObject);
var
  oSmtp: TMail;
  i: integer;
  Rcpts: OleVariant;
  RcptBound: integer;
  RcptAddr: WideString;
  oEncryptCert: TCertificate;
begin

  if trim(textFrom.Text) = '' then
  begin
    ShowMessage('Plese input From email address!');
    textFrom.SetFocus();
    exit;
  end;

  if(trim(textTo.Text) = '') and
     (trim(textCc.Text) = '') then
  begin
    ShowMessage('Please input To or Cc email addresses, please use comma(,) to separate multiple addresses!');
    textTo.SetFocus();
    exit;
  end;

  if chkAuth.Checked and ((trim(textUser.Text)='') or
  (trim(textPassword.Text)='')) then
  begin
    ShowMessage('Please input User, Password for SMTP authentication!');
    textUser.SetFocus();
    exit;
  end;

  try
    // Create TMail Object
    oSmtp := TMail.Create(Application);
    oSmtp.LicenseCode := 'TryIt';
  except on E: exception do
    begin
      ShowMessage(E.Message);
      exit;
    end;
  end;
  
  btnSend.Enabled := false;

  oSmtp.Charset := m_arCharset[lstCharset.ItemIndex, 1];
  oSmtp.FromAddr := ChAnsiToWide(trim(textFrom.Text));

  // Add recipient's
  oSmtp.AddRecipientEx(ChAnsiToWide(trim(textTo.Text)), 0);
  oSmtp.AddRecipientEx(ChAnsiToWide(trim(textCc.Text)), 0);

  // Set subject
  oSmtp.Subject := ChAnsiToWide(textSubject.Text);

  // Using HTML FORMAT to send mail
  // oSmtp.BodyFormat := 1;

  // Set body
  oSmtp.BodyText := ChAnsiToWide(textBody.Text);

  // Add attachments
  for i:= 0 to m_arAttachments.Count - 1 do
  begin
      oSmtp.AddAttachment(ChAnsiToWide(m_arAttachments[i]));
  end;

  // Add digital signature
  if chkSign.Checked then
  begin
    if not oSmtp.SignerCert.FindSubject(oSmtp.FromAddr,
    CERT_SYSTEM_STORE_CURRENT_USER, 'my') then
    begin
      ShowMessage('Not cert found for signing: ' + oSmtp.SignerCert.GetLastError());
      btnSend.Enabled := true;
      exit;
    end;

    if not oSmtp.SignerCert.HasCertificate Then
    begin
      ShowMessage('Signer certificate has no private key, ' +
      'this certificate can not be used to sign email');
       btnSend.Enabled := true;
       exit;
    end;
  end;

  // get all to, cc, bcc email address to an array
  Rcpts := oSmtp.Recipients;
  RcptBound := VarArrayHighBound(Rcpts, 1);
  // search encrypting cert for every recipient.
  if chkEncrypt.Checked then
    for i := 0 to RcptBound do
    begin
       RcptAddr := VarArrayGet(Rcpts, i);
       oEncryptCert := TCertificate.Create(Application);
       if not oEncryptCert.FindSubject(RcptAddr,
          CERT_SYSTEM_STORE_CURRENT_USER, 'AddressBook') then
          if not oEncryptCert.FindSubject(RcptAddr,
            CERT_SYSTEM_STORE_CURRENT_USER, 'my') then
          begin
            ShowMessage('Failed to find cert for ' + RcptAddr + ': ' + oEncryptCert.GetLastError());
            btnSend.Enabled := true;
            exit;
          end;

       oSmtp.RecipientsCerts.Add(oEncryptCert.DefaultInterface);
    end;

  oSmtp.ServerAddr := trim(textServer.Text);
  oSmtp.Protocol := lstProtocol.ItemIndex;

  // Most mordern SMTP servers require SSL/TLS connection now
  // ConnectTryTLS means if server supports SSL/TLS connection, SSL/TLS is used automatically
	oSmtp.ConnectType := ConnectTryTLS;

  if oSmtp.ServerAddr <> '' then
  begin
    if lstPort.ItemIndex = 2 then
      oSmtp.ServerPort := 465
    else if lstPort.ItemIndex = 1 then
      oSmtp.ServerPort := 587
    else
      oSmtp.ServerPort := 25;

    if chkAuth.Checked then
    begin
      oSmtp.UserName := trim(textUser.Text);
      oSmtp.Password := trim(textPassword.Text);
    end;

    if chkSSL.Checked then
    begin
      // Use SSL/TLS based on server port.
			oSmtp.ConnectType := ConnectSSLAuto;
    end;
  end;

  if (RcptBound > 0) and (oSmtp.ServerAddr = '') then
  begin
    // To send email without specified smtp server, we have to send the emails one by one
    // to multiple recipients. That is because every recipient has different smtp server.
    DirectSend(oSmtp);
    btnSend.Enabled := true;
    exit;
  end;

  if oSmtp.SendMail() = 0 then
    ShowMessage('Message delivered')
  else
    ShowMessage(oSmtp.GetLastErrDescription());

  btnSend.Enabled := true;
end;

procedure TForm1.DirectSend(oSmtp: TMail);
var
  Rcpts: OleVariant;
  i, RcptBound: integer;
  RcptAddr: WideString;
begin
  Rcpts := oSmtp.Recipients;
  RcptBound := VarArrayHighBound(Rcpts, 1);
  for i := 0 to RcptBound do
  begin
    RcptAddr := VarArrayGet(Rcpts, i);
    oSmtp.ClearRecipient();
    oSmtp.AddRecipientEx(RcptAddr, 0);
    ShowMessage('Start to send email to ' + RcptAddr);
    if oSmtp.SendMail() = 0 then
      ShowMessage('Message delivered to ' +  RcptAddr + ' successfully!')
    else
      ShowMessage('Failed to deliver to ' + RcptAddr + ': ' + oSmtp.GetLastErrDescription());
  end;
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
// In new version of Delphi, string is defined as WideString by default,
// So use this overload function to bypass the compilation error and keep back compatiable.
function TForm1.ChAnsiToWide(const StrA: WideString): WideString;
begin
  Result := StrA;
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
end;

procedure TForm1.btnClearClick(Sender: TObject);
begin
  m_arAttachments.Clear();
  textAttachment.Text := '';
end;

procedure TForm1.FormResize(Sender: TObject);
begin
  if Form1.Width < 671 then
    Form1.Width := 671;

  if Form1.Height < 445 then
    Form1.Height := 445;

  textBody.Width := Form1.Width - 35;
  textBody.Height := Form1.Height - 300;
  btnSend.Top := textBody.Top +  textBody.Height + 5;
  btnSend.Left := Form1.Width - 25 - btnSend.Width;
  GroupBox1.Left := Form1.Width - GroupBox1.Width - 25;
  textFrom.Width := Form1.Width - GroupBox1.Width - 110;
  textSubject.Width := textFrom.Width;
  textTo.Width := textFrom.Width;
  textCc.Width := textFrom.Width;

end;

procedure TForm1.chkAuthClick(Sender: TObject);
begin
  textUser.Enabled := chkAuth.Checked;
  textPassword.Enabled := chkAuth.Checked;

  if(chkAuth.Checked) then
  begin
    textUser.Color := clWindow;
    textPassword.Color := clWindow;
  end
  else
  begin
    textUser.Color := cl3DLight;
    textPassword.Color := cl3DLight;
  end;
end;

procedure TForm1.textFromChange(Sender: TObject);
var
  domain: string;
begin
  if m_autoChangeUser Then
    textUser.Text := GetSenderAddr(textFrom.Text);

  if m_autoChangeServer then
  begin
    domain := GetSenderDomain(textFrom.Text);

    if CompareText(domain, 'hotmail.com') = 0 then
      textServer.Text := 'smtp.live.com'
    else if CompareText(domain, 'gmail.com') = 0 then
      textServer.Text :='smtp.gmail.com'
    else if CompareText(domain, 'yahoo.com') = 0 then
      textServer.Text :='smtp.mail.yahoo.com'
    else if CompareText(domain, 'aol.com') = 0 then
      textServer.Text :='smtp.aol.com'
  end;

end;

procedure TForm1.textServerChange(Sender: TObject);
begin
  ChangeSettingForWellKnownServer();
end;

function TForm1.GetSenderDomain(const s: string): string;
var
  p: integer;
  domain: string;
begin
  Result := '';
  p := Pos('@', s);
  if p = 0 then
    exit;

  domain := Trim(MidStr(s, p+1, Length(s) - p));
  if(Length(domain) = 0)then
    exit;

  if Pos('>', domain) = Length(domain) then
    domain := LeftStr(domain, Length(domain)-1);

  if Length(domain) > 0 then
    Result := domain;
end;

function TForm1.GetSenderAddr(const s: string): string;
  var
    p: integer;
    addr: string;
begin
  Result := Trim(s);
  p := Pos('<', s);
  if p = 0 then
    exit;

  addr := MidStr(s, p+1, Length(s) - p);
  p := Pos('>', addr);
  Result := addr;
  if p = 0 then
    exit;

  addr := MidStr(addr, 1, p-1);
  Result := addr;
end;

procedure TForm1.ChangeSettingForWellKnownServer();
var
  server: string;
begin
  server := textServer.Text;
  if (CompareText(server, 'smtp.live.com') = 0) Or
    (CompareText(server, 'smtp.gmail.com') = 0) Or
    (CompareText(server, 'smtp.office365.com')= 0) Or
    (CompareText(server, 'smtp.mail.yahoo.com') = 0) Or
    (CompareText(server, 'smtp.aol.com') = 0) then
  begin
    chkSSL.Checked := true;
    chkAuth.Checked := true;
    lstPort.ItemIndex := 1;  // using 587 port, 25, 465 is also avaiable.
  end;
end;

procedure TForm1.textServerKeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
  if textServer.Text = '' then
    m_autoChangeServer := true
  else
    m_autoChangeServer := false;

end;

procedure TForm1.textUserKeyUp(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
  if textUser.Text = '' then
    m_autoChangeUser := true
  else
    m_autoChangeUser := false;

end;

procedure TForm1.lstProtocolClick(Sender: TObject);
begin
  // EWS always requires SSL by default.
  if lstProtocol.ItemIndex = 1 then
    chkSSL.Checked := true;

end;

end.
