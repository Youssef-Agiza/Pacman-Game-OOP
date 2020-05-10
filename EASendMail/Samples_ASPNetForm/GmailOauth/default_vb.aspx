<%@ Page Language="VBScript" AutoEventWireup="true" ValidateRequest="false" CodePage="65001" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="EASendMail" %>

<script language="vbscript" runat="server">
'  ===============================================================================
' |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
' |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
' |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
' |    PARTICULAR PURPOSE.                                                    |
' |    Copyright (c)2015  ADMINSYSTEM SOFTWARE LIMITED                        |
' |
' | FILE: default.aspx
' | SYNOPSIS: Sample ASP.NET for Gmail OAUTH.
' | Ivan Lui (Ivan@EmailArchitect.NET)
' |
'
'     To use Google OAUTH in your application, you must create a project in Google Developers Console.
'
'    - Create your project at https://console.developers.google.com/project.
'    - Select your project -> APIs & Services -> Dashboard -> Credentials;
'    - Credentials -> Create Credentials -> OAuth client ID -> Web application or Other (Desktop Application). 
'      It depends on your application type.
'
'    - Input a name for your application, input your current ASP/ASP.NET URL at Authorized redirect URIs, 
'      for example: http://localhost/gmailoauth/default_vb.aspx. (Desktop Application doesn't require this step)
'      Click "Create", you will get your client id and client secret.
'
'    - Finally you can also set detail information for your project at Credentials -> OAuth consent screen.

'    - If you used https://mail.google.com scope, you should verify your application that is inroduced in cosent screen.
'      If you don't verify your application, your application is limited by some conditions.

'      You must apply for your client id and client secret, don't use the client id in the sample project, because it is limited now.
'      If you got "This app isn't verified" information, please click "advanced" -> Go to ... for test.

'    */
' ===============================================================================================================

Private Shared m_client_id As String = "1072602369179-prgq7tkc834li6ao3fd11r6f2mpc1hdq.apps.googleusercontent.com"
Private Shared m_client_secret As String = "zRqEjYMLJwvfacYA_vUH_6M4"

Private Shared m_redirect_uri As String = "http://localhost/gmailoauth/default_vb.aspx" 'please change it to your current uri
Private Shared m_scope As String = "email%20profile%20https://mail.google.com"
Private Shared m_auth_uri As String = "https://accounts.google.com/o/oauth2/v2/auth"
Private Shared m_token_uri As String = "https://www.googleapis.com/oauth2/v4/token"
Private Shared m_plus_api_uri As String = "https://www.googleapis.com/plus/v1/people/me?fields=emails&access_token="

Private Sub Page_Load(sender As [Object], e As EventArgs)
	If txtAccessToken.Value.Length = 0 Then
		Dim code As String = Request.QueryString("code")
		If Not (code Is Nothing) Then
			If code.Length > 0 Then
                GetAccessToken(code)
			End If
		Else
			Dim authURI As String = [String].Format("{0}?scope={1}&redirect_uri={2}&response_type=code&client_id={3}&prompt=consent&access_type=offline", m_auth_uri, m_scope, m_redirect_uri, m_client_id)
			Response.Redirect(authURI)
			Response.[End]()
		End If
	End If

	If lstPort.Items.Count > 0 Then
		Return
	End If

	lstPort.Items.Add("25")
	lstPort.Items.Add("587")
	lstPort.Items.Add("465")

	lstPort.SelectedIndex = 1

	txtSubject.Text = "Gmail Oauth ASP.Net Test"
	Dim s As String = "Hi," & vbCr & vbLf & "This sample demonstrates how to send email in ASP.NET." & vbCr & vbLf & vbCr & vbLf
	s += "From:[$from]" & vbCr & vbLf
	s += "To:[$to]" & vbCr & vbLf
	s += "Subject:[$subject]" & vbCr & vbLf & vbCr & vbLf
	s += "This sample project demonstrates how to send email using Gmail OAUTH, " & vbCr & vbLf
	s += "Please create your client_id and client_secret introduced in source code." & vbCr & vbLf & vbCr & vbLf
	s += "If you got ""This app isn't verified"" information, please click ""advanced"" -> Go to ... for test."
	s += vbCr & vbLf

	txtBody.Text = s
End Sub

private Function IsAccessTokenExpired() As Boolean
	Dim AccessTokenTimeStamp As DateTime =  DateTime.Parse(txtAccessTokenTimeStamp.Value)
    Dim TokenExpiresInSeconds As Integer = Int32.Parse(txtTokenExpiresInSeconds.Value)
	IsAccessTokenExpired = (AccessTokenTimeStamp.AddSeconds(TokenExpiresInSeconds - 30) < DateTime.Now)
End Function

' Get access_token from Google server.
Private Sub GetAccessToken(code As String)
	Dim encoder As System.Text.Encoding = System.Text.Encoding.GetEncoding("utf-8")

	Try
		Dim url As String = m_token_uri
		Dim oClient As HttpWebRequest = TryCast(WebRequest.Create(url), HttpWebRequest)
		oClient.Method = "POST"
		oClient.ContentType = "application/x-www-form-urlencoded"

		Dim client_id As String = m_client_id
		Dim client_secret As String = m_client_secret
		Dim redirect_uri As String = m_redirect_uri

		Dim data As String = "code=" & code
		data += "&client_id=" & client_id
		data += "&client_secret=" & client_secret
		data += "&redirect_uri=" & redirect_uri
		data += "&grant_type=authorization_code"

		Dim ofs As Stream = oClient.GetRequestStream()

		Dim dt As Byte() = System.Text.Encoding.UTF8.GetBytes(data)
		ofs.Write(dt, 0, dt.Length)
		ofs.Close()

		Dim oResponse As HttpWebResponse = TryCast(oClient.GetResponse(), HttpWebResponse)

		Dim readStream As New System.IO.StreamReader(oResponse.GetResponseStream(), encoder)
		Dim responseText As String = readStream.ReadToEnd()
		
	   	Dim parser As new OAuthResponseParser()
		parser.Load(responseText)

		txtAccessToken.Value = parser.AccessToken
		txtRefreshToken.Value = parser.RefreshToken
		txtTokenExpiresInSeconds.Value = parser.TokenExpiresInSeconds.ToString()
		txtAccessTokenTimeStamp.Value = DateTime.Now.ToString()
		txtUser.Value = parser.EmailInIdToken
		txtFrom.Text = parser.EmailInIdToken

		if (txtAccessToken.Value.Length = 0) Or (txtUser.Value.Length = 0) then
            Dim output As String = "<pre>OAUTH Error:" & vbCr & vbLf & vbCr & vbLf
			output += Server.HtmlEncode(responseText)
			output += "</pre>" & vbCr & vbLf

			Response.Write(output)
			Response.[End]()
			Return
		End If
		
	Catch ex As WebException
		Dim errorDesc As String = ex.Message
		If (ex.Response IsNot Nothing) Then
			Dim readStream As New System.IO.StreamReader(ex.Response.GetResponseStream(), encoder)
			errorDesc = readStream.ReadToEnd()
		End If
		Dim output As String = "<pre>OAUTH Error:" & vbCr & vbLf & vbCr & vbLf
		output += Server.HtmlEncode(errorDesc)
		output += "</pre>" & vbCr & vbLf

		Response.Write(output)
		Response.[End]()
	End Try
    
End Sub

private Sub  RefreshToken()
	Dim encoder As System.Text.Encoding = System.Text.Encoding.GetEncoding("utf-8")

	Try
		Dim url As String = m_token_uri
		Dim oClient As HttpWebRequest = TryCast(WebRequest.Create(url), HttpWebRequest)
		oClient.Method = "POST"
		oClient.ContentType = "application/x-www-form-urlencoded"

		Dim client_id As String = m_client_id
		Dim client_secret As String = m_client_secret
		Dim redirect_uri As String = m_redirect_uri

		Dim data As String = "client_id=" & client_id
		data += "&client_secret=" & client_secret
		data += "&redirect_uri=" & redirect_uri
		data += "&refresh_token=" & txtRefreshToken.Value
        data += "&grant_type=refresh_token"

		Dim ofs As Stream = oClient.GetRequestStream()

		Dim dt As Byte() = System.Text.Encoding.UTF8.GetBytes(data)
		ofs.Write(dt, 0, dt.Length)
		ofs.Close()

		Dim oResponse As HttpWebResponse = TryCast(oClient.GetResponse(), HttpWebResponse)

		Dim readStream As New System.IO.StreamReader(oResponse.GetResponseStream(), encoder)
		Dim responseText As String = readStream.ReadToEnd()

	   	Dim parser As new OAuthResponseParser()
		parser.Load(responseText)

		txtAccessToken.Value = parser.AccessToken
		txtTokenExpiresInSeconds.Value = parser.TokenExpiresInSeconds.ToString()
		txtAccessTokenTimeStamp.Value = DateTime.Now.ToString()

	Catch ex As WebException
		Dim errorDesc As String = ex.Message
		If (ex.Response IsNot Nothing) Then
			Dim readStream As New System.IO.StreamReader(ex.Response.GetResponseStream(), encoder)
			errorDesc = readStream.ReadToEnd()
		End If
		Dim output As String = "<pre>Refresh Token Error:" & vbCr & vbLf & vbCr & vbLf
		output += Server.HtmlEncode(errorDesc)
		output += "</pre>" & vbCr & vbLf

		Response.Write(output)
		Response.[End]()
	End Try
    
End Sub 

Private Sub btnSend_Click(sender As Object, e As System.EventArgs)

	If IsAccessTokenExpired() Then
		RefreshToken()
	End if

	result.Text = ""

	If txtTo.Text.Trim() = "" Then
		lblDesc.Text = "Please input recipient address!"
		Return
	End If

	lblDesc.Text = ""

	'For evaluation usage, please use "TryIt" as the license code, otherwise the 
	'"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
	'"trial version expired" exception will be thrown.

	'For licensed uasage, please use your license code instead of "TryIt", then the object
	'will never expire
	Dim oMail As New SmtpMail("TryIt")

	oMail.Charset = "utf-8"

	oMail.From = txtFrom.Text
	oMail.Subject = txtSubject.Text

	'To, Cc and Bcc is a AddressCollection object, in C#, it supports implicit converting from string.
	' multiple address are separated with (,;)
	'The syntax is like this: "test@adminsystem.com, test1@adminsystem.com"

	'The example code without implicit converting
	' oMail.To = new AddressCollection("test1@adminsystem.com, test2@adminsystem.com");
	' oMail.To = new AddressCollection("Tester1<test@adminsystem.com>, Tester2<test2@adminsystem.com>");		
	oMail.[To] = txtTo.Text
	'You can add more recipient by Add method
	' oMail.To.Add(new MailAddress("tester", "test@adminsystem.com"));


	Dim fileName As String = Nothing
	If attachment.PostedFile IsNot Nothing Then
		fileName = attachment.PostedFile.FileName
		If fileName IsNot Nothing AndAlso fileName <> "" Then
			Try
				Dim fileLen As Integer = attachment.PostedFile.ContentLength
				Dim content As Byte() = New Byte(fileLen - 1) {}
				Dim stream As System.IO.Stream = attachment.PostedFile.InputStream
				stream.Read(content, 0, fileLen)
				stream.Close()
				oMail.AddAttachment(fileName, content)
			Catch exec As Exception
				lblDesc.Text = [String].Format("Exception with add attachment: {0}", exec.ToString())
				Return
			End Try
		End If
	End If

	Dim body As String = txtBody.Text
	body = body.Replace("[$from]", txtFrom.Text)
	body = body.Replace("[$to]", txtTo.Text)
	body = body.Replace("[$subject]", oMail.Subject)


	oMail.TextBody = body

	Dim oSmtp As New SmtpClient()
	'To generate a log file for SMTP transaction, please use
	'oSmtp.LogFileName = "c:\\smtp.log";

	Dim oServer As New SmtpServer(txtServer.Text)
	oServer.Protocol = ServerProtocol.SMTP
	oServer.ConnectType = SmtpConnectType.ConnectSSLAuto
	oServer.Port = Int32.Parse(lstPort.Text)
	oServer.AuthType = SmtpAuthType.XOAUTH2

	oServer.User = txtUser.Value
	oServer.Password = txtAccessToken.Value

	Dim err As String = ""
	Dim bSmtpConversation As Boolean = False
	Try

		bSmtpConversation = True
		oSmtp.SendMail(oServer, oMail)

		result.Text = [String].Format("The message was sent to {0} successfully!<br />", oServer.Server)
	Catch exp As SmtpTerminatedException
		err = exp.Message
	Catch exp As System.Exception
		err = [String].Format("Exception: {0}", exp.Message)
	End Try

	If err.Length > 0 Then
		result.Text = Server.HtmlEncode(err)
	End If

	If bSmtpConversation Then
		result.Text += "<pre>SMTP Log" & vbCr & vbLf & vbCr & vbLf
		result.Text += Server.HtmlEncode(oSmtp.SmtpConversation)
		result.Text += "</pre>" & vbCr & vbLf
	End If

End Sub

</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ASP.NET, VB Sample For EASendMail</title>
    <meta http-equiv="Content-Type" content="text-html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="sample.css" />
</head>
<body>
    <div id="s_title">
        ASP.NET, VB.NET Sample for EASendMail SMTP Component</div>
    <form id="thisForm" enctype="multipart/form-data" method="post" runat="server">
    <div id="s_result">
        <asp:Literal ID="result" Text="" runat="server" />
    </div>
    <div id="div_main">
        <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td>
                    Notice:
                </td>
                <td>
                <asp:Label ID="lblDesc" runat="server" ForeColor="red"></asp:Label>
                    <div class="comments">
                    Please create your client_id, client_secret at <a href="at https://console.developers.google.com/project" target="_blank">at https://console.developers.google.com/project</a> introduced in source codes.    
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    SMTP Server:
                </td>
                <td>
                    <asp:TextBox ID="txtServer" runat="server" Width="95%" Text="smtp.gmail.com"></asp:TextBox>
					<asp:HiddenField ID="txtAccessToken" runat="server" />
					<asp:HiddenField ID="txtRefreshToken" runat="server" />
                    <asp:HiddenField ID="txtAccessTokenTimeStamp" runat="server" />
                    <asp:HiddenField ID="txtTokenExpiresInSeconds" runat="server" />
                    <asp:HiddenField ID="txtUser" runat="server" />
                </td>
            </tr>
           
            <tr>
                <td>
                    Server Port:
                </td>
                <td>
                    <asp:DropDownList ID="lstPort" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td>
                    *From:
                </td>
                <td>
                    <asp:TextBox ID="txtFrom" runat="server" Width="95%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    *To:
                </td>
                <td>
                    <div class="comments">
                        Please separate multiple recipients with comma(,).</div>
                    <asp:TextBox ID="txtTo" size="50" runat="server" Width="95%"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    Subject:
                </td>
                <td>
                    <asp:TextBox ID="txtSubject" runat="server" Width="95%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Attachment:
                </td>
                <td>
                    <input type="File" id="attachment" name="attachment" runat="server">
                </td>
            </tr>
            <tr>
                <td>
                    Email Body
                </td>
                <td>
                    <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Width="95%" Height="121px"></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnSend" runat="server" Width="131px" Text="Send" OnClick="btnSend_Click">
                    </asp:Button>
                    - <a href="default_vb.aspx">Reset</a>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <div id="tailer">
        Technical Support: <a href="mailto:support@emailarchitect.net">support@emailarchitect.net</a>
        <br />
        <br />
        <a href="http://www.emailarchitect.net" target="_blank">2006 - 2019 Copyright &copy;
            AdminSystem Software Limited. All rights reserved.</a>
    </div>
</body>
</html>
