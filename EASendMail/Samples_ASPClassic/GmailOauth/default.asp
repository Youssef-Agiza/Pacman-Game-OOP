<%@  codepage="65001" language="VBScript" %>
<%
' ===============================================================================
' |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
' |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
' |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
' |    PARTICULAR PURPOSE.                                                    |
' |    Copyright (c)2010 - 2019 ADMINSYSTEM SOFTWARE LIMITED                         |
' |
' | FILE: Default.ASP
' | SYNOPSIS: SAMPLE FILE FOR EASendMail ActiveX Object (VBScript)
' |
'     The client_id and client_secret is created for test purposes, please create your clientid
'     To use Google OAUTH in your application, you must create a project in Google Developers Console.
'
'    - Create your project at https://console.developers.google.com/project.
'    - Select your project -> APIs & Services -> Dashboard -> Credentials;
'    - Credentials -> Create Credentials -> OAuth client ID -> Web application or Other (Desktop Application).
'      It depends on your application type.

'    - Input a name for your application, input your current ASP/ASP.NET URL at Authorized redirect URIs,
'      For example: http://localhost/gmailoauth/default.asp. (Desktop Application doesn't require this step)
'      Click "Create", you will get your client id and client secret.
'
'    - Finally you can also set detail information for your project at Credentials -> OAuth consent screen.

'    - If you used https://mail.google.com scope, you should verify your application that is inroduced in cosent screen.
'      If you don't verify your application, your application is limited by some conditions.

'      You must apply for your client id and client secret, don't use the client id in the sample project, because it is limited now.
'      If you got "This app isn't verified" information, please click "advanced" -> Go to ... for test.

' Any problem, please kindly contact support@emailarchitect.net
' =================================================================================*/
Response.CharSet = "utf-8" 

Const ConnectNormal = 0
Const ConnectSSLAuto = 1
Const ConnectSTARTTLS = 2
Const ConnectDirectSSL = 3
Const ConnectTryTLS = 4

' The following clientId and clientSecret is only for test purpose,
' You should apply for your clientId and clientSecret in production environment.

Const m_client_id = "1072602369179-prgq7tkc834li6ao3fd11r6f2mpc1hdq.apps.googleusercontent.com"
Const m_client_secret = "zRqEjYMLJwvfacYA_vUH_6M4"

Const m_redirect_uri = "http://localhost/gmailoauth/default.asp"
Const m_scope = "email%20profile%20https://mail.google.com"
Const m_auth_uri = "https://accounts.google.com/o/oauth2/v2/auth"
Const m_token_uri = "https://www.googleapis.com/oauth2/v4/token"

'========================================================
' fnParseAddr
'========================================================
Function fnParseAddr(src, Byref name, Byref addr)
	Dim nIndex
	nIndex = InStr(1, src, "<")
	If nIndex > 0 Then
		name = Mid(src, 1, nIndex - 1)
		addr = Mid(src, nIndex)
	Else
		name = ""
		addr = src
	End If
	
	Call fnTrim(name, " ,;<>""'")
	Call fnTrim(addr, " ,;<>""'")
End Function
'========================================================
' fnTrim
'========================================================
Function fnTrim(Byref src, trimer)
	Dim i, nCount, ch
	nCount = Len(src)
	For i = 1 To nCount
		ch = Mid(src, i, 1)
		If InStr(1, trimer, ch) < 1 Then
			Exit For
		End If
	Next
	
	src = Mid(src, i)
	nCount = Len(src)
	For i = nCount To 1 Step -1
		ch = Mid(src, i, 1)
		If InStr(1, trimer, ch) < 1 Then
			Exit For
		End If	
	Next
	src = Mid(src, 1, i)
End Function

'========================================================
' strpbrk
'========================================================
Function strpbrk(src, start, charset)
	strpbrk = 0
	
	Dim i, size, pos, ch
	
	size = Len(src)
	For i = start To size
		ch = Mid(src, i, 1)
		
		If InStr(1, charset, ch) >= 1 Then
			strpbrk = i
			Exit Function
		End If
	Next

End Function

'========================================================
' SplitEx
'========================================================
Function SplitEx(src, charset)

	Dim find, start, pos, bquoted, ch, item, s
	Dim arItems()
	Dim nItems, nBuf, nIndex
	
	nIndex	= 0
	nBuf	= 10
	
	ReDim Preserve arItems(	nBuf)
	
	find	= charset & """"
	start	= 1
	pos		= 1
	bquoted	= False
		
	Do While True
		pos = strpbrk(src, pos, find)
		
		If pos <= 0 Then
			s = Mid(src, start) 
			fnTrim s, charset
			If s <> "" Then
				If nIndex >= nBuf - 1 Then
					nBuf = nBuf + 10
					ReDim Preserve arItems(	nBuf)
				End If		
				arItems(nIndex) = s
			End If
			Exit Do
		End If
		
		ch = Mid(src, pos, 1)

		If ch = """" Then
			If bquoted Then
				bquoted = False
			Else
				bquoted = True
			End If
		End If
		
		If Not bquoted And ch <> """" Then
			s = Mid(src, start, pos - start) 
			fnTrim s, charset
			If s <> "" Then
				If nIndex >= nBuf - 1 Then
					nBuf = nBuf + 10
					ReDim Preserve arItems(	nBuf)
				End If			
				arItems(nIndex) = s
				nIndex = nIndex + 1
			End If
			pos		= pos + 1
			start	= pos
		Else
			pos		= pos + 1
		End If
	Loop
	
	ReDim Preserve arItems(nIndex)
	SplitEx = arItems
End Function



Sub SendMail()
	Dim from, recipients
	from = Request.Form("from")
	recipients = Request.Form("recipients")
	
	If from = "" Or recipients = "" Then
		Response.Write("<font color=red>Please input from, to</font>")
		Exit Sub
	End If
	
	Dim oSmtp
	Set oSmtp = Server.CreateObject("EASendMailObj.Mail")
    'The license code for EASendMail ActiveX Object,
    'for evaluation usage, please use "TryIt" as the license code.	
	oSmtp.LicenseCode = "TryIt"
	
	oSmtp.Charset = "utf-8"
	
	Dim serveraddr
	
	serveraddr = Request.Form("serveraddr")
	oSmtp.ServerAddr = serveraddr
	oSmtp.Protocol = 0 'SMTP Protocol
	oSmtp.ServerPort = Request.Form("serverport")
    oSmtp.AuthType = 5 ' GMAIL OAUTH

    oSmtp.UserName = m_user
    oSmtp.Password = m_access_token

    ' Use SSL/TLS based on server port.
	oSmtp.ConnectType = ConnectSSLAuto

	Dim name, addr
	fnParseAddr from, name, addr
	oSmtp.FromAddr	= m_user

    If LCase(addr) <> m_user Then
        oSmtp.ReplyTo = addr
    End If
	
	'Using this email to be replied to another address 
	'oSmtp.ReplyTo = ReplyAddress 

	oSmtp.AddRecipientEx recipients, 0  ' Normal recipient 
	'oSmtp.AddRecipient CCName, CCEmailAddress, 1  'CC 
	'oSmtp.AddRecipient BCCName, BCCEmailAddress, 2 'BCC 


	'Attachs file to this email 
	'oSmtp.AddAttachment "c:\test.txt"
	
	Dim subject, bodytext
	subject = Request.Form("subject")
	bodytext = Request.Form("bodytext") 
	bodytext = Replace(bodytext, "[$from]", from)
	bodytext = Replace(bodytext, "[$to]", recipients)
	bodytext = Replace(bodytext, "[$subject]", subject)
	
	oSmtp.Subject	= subject
	oSmtp.BodyText	= bodytext 
	
	
	If oSmtp.SendMail() = 0 Then
		Response.Write "Message delivered." 
	Else 
		Response.Write "<font color=red>" & Server.HTMLEncode(oSmtp.GetLastErrDescription()) & "</font>" 'Get last error description 
	End If
End Sub

Dim m_oRequest


Sub GetAccessToken(code)
    
    Set m_oRequest = Server.CreateObject("MSXML2.ServerXMLHTTP")
    Dim data
    data =  "code=" & code
    data = data & "&client_id=" & m_client_id
    data = data & "&client_secret=" & m_client_secret
    data = data & "&redirect_uri=" & m_redirect_uri
    data = data & "&grant_type=authorization_code"    

	m_oRequest.setOption 2, 13056 
    m_oRequest.open "POST", m_token_uri, false 
    m_oRequest.setRequestHeader "Content-Type", "application/x-www-form-urlencoded"
	m_oRequest.send data 
	
    Dim status
    status = m_oRequest.status
    
    If status < 200 Or status >= 300 Then
        Response.Write("Failed to get access token from Google server.")
        Response.End
    End If	

    Dim result
    result = m_oRequest.responseText

    Dim oauthParser
    Set oauthParser = CreateObject("EASendMailObj.OAuthResponseParser")
    oauthParser.Load result
    
    m_access_token = oauthParser.AccessToken
    m_refresh_token = oauthParser.RefreshToken
    m_user = oauthParser.EmailInIdToken

End Sub

Dim m_access_token, m_refresh_token, m_user
m_access_token = Request.Form("accesstoken")
m_user = Request.Form("user")

Dim code, authURI
code = Request.QueryString("code")
If m_access_token = "" Then
    If code <> "" Then
        GetAccessToken code
    Else
        authURI = m_auth_uri & "?scope=" & m_scope & "&redirect_uri=" & m_redirect_uri & "&response_type=code&client_id=" & m_client_id & "&prompt=login"
        ' If you want to get refresh token, please change it to the following value
        ' authURI = m_auth_uri & "?scope=" & m_scope & "&redirect_uri=" & m_redirect_uri & "&response_type=code&client_id=" & m_client_id & "&access_type=offline&prompt=consent"               
		Response.Redirect(authURI)
    End If
End If

Dim from_addr
from_addr = Request.Form("from")
If from_addr = "" Then
    from_addr = m_user        
End If

%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ASP, VBScript Sample For EASendMail ActiveX Object (Gmail OAUTH)</title>
    <meta http-equiv="Content-Type" content="text-html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="sample.css" />
</head>
<body>
    <div id="s_title">
        ASP, VBScript Sample For EASendMail ActiveX Object (Gmail OAUTH)</div>
    <form name="thisForm" method="post" action="default.asp">
    <div id="div_main">
    <%

    If Request.ServerVariables("REQUEST_METHOD") = "POST" Then
	    Response.Write("<div id=""s_info"">")
	    SendMail         

	    Response.Write("</div>")
    End If 
%>
        
        <div class="comments">
            Please create your client_id, client_secret at <a href="at https://console.developers.google.com/project" target="_blank">at https://console.developers.google.com/project</a> introduced in source codes.
        </div>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="150">
                    SMTP Server:
                </td>
                <td>
                    <input type="text" name="serveraddr" style="width: 95%;" value="smtp.gmail.com"   />
                    <input type="hidden" name="accesstoken" value="<%=m_access_token%>" />
                    <input type="hidden" name="user" value="<%=m_user%>"/>
                </td>
            </tr>
            <tr>
                <td>
                    Port:
                </td>
                <td>
                    <select name="serverport">
                        <option value="25">25</option>
                        <option value="587" selected="selected">587</option>
                        <option value="465">465</option>
                    </select>
                </td>
            </tr>
           
            <tr>
                <td>
                    From:
                </td>
                <td>
                    <input type="text" name="from" style="width:95%;" value="<%=Server.HTMLEncode(from_addr)%>" />
                </td>
            </tr>
            <tr>
                <td>
                    To:
                </td>
                <td>
                    <div class="comments">
                    Please separate multiple recipients with comma(,)</div>
                    <input type="text" name="recipients" style="width:95%;" value="<%=Server.HTMLEncode(Request.Form("recipients"))%>" />
                </td>
            </tr>

            
            <tr>
                <td>
                    Subject
                </td>
                <td>
                    <input type="text" name="subject" value="Classic Asp Gmail OAuth Test" style="width:95%;" />
                </td>
            </tr>

            <tr>
                <td valign="top">
                     Body Text
                </td>
                <td>
                    <textarea name="bodytext" cols="50" rows="8" style="width:95%;">
From: [$from]
To: [$to]
Subject: [$subject]

This is a test email from ASP Gmail OAUTH sample.

You must apply for your client id and client secret, don't use the client id in the sample project, because it is limited now.
If you got "This app isn't verified" information, please click "advanced" -&gt; Go to ... for test.
						</textarea>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <input type="submit" name="submit1" value=" Send Mail " /> - <a href="default.asp">Reset</a>
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
