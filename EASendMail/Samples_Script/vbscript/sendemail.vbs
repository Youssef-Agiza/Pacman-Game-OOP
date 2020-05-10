'  ===============================================================================
' |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
' |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
' |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
' |    PARTICULAR PURPOSE.                                                    |
' |    Copyright (c)2010 - 2019  ADMINSYSTEM SOFTWARE LIMITED                        |
' |
' |    Project: It demonstrates how to use EASendMail to send email in VBScript
' |
' |    Author: Ivan Lui (ivan@emailarchitect.net)
'  ===============================================================================

Const ConnectNormal = 0
Const ConnectSSLAuto = 1
Const ConnectSTARTTLS = 2
Const ConnectDirectSSL = 3
Const ConnectTryTLS = 4

Sub Send(From, Recipient, Subject, Body, Server)
	
	NORMAL_RECIPIENT = 0
	COPY_RECIPIENT = 1
	BLIND_COPY_RECIPIENT = 2

	TEXT_PLAIN = 0
	TEXT_HTML = 1	

	Dim oSmtp
	Set oSmtp = CreateObject("EASendMailObj.Mail")
	'The license code for EASendMail ActiveX Object,
	'for evaluation usage, please use "TryIt" as the license code.
	oSmtp.LicenseCode = "TryIt"
	
	oSmtp.Reset
	oSmtp.FromAddr = From
	oSmtp.Subject = Subject
	oSmtp.BodyFormat = TEXT_PLAIN
	oSmtp.BodyText = Body
	oSmtp.ServerAddr = Server

	' If your server requires user authentication
	' | ESMTP authentication
	' oSmtp.UserName = "your user name"
	' oSmtp.Password = "your password"

    ' Set server port, if 25 port doesn't work, try to use 587 port
    oSmtp.ServerPort = 25

    ' Most mordern SMTP servers require SSL/TLS connection now
    ' ConnectTryTLS means if server supports SSL/TLS connection, SSL/TLS is used automatically
    oSmtp.ConnectType = ConnectTryTLS

    ' If your server is Exchange 2007 or later version, you can use EWS protocol.
    ' https://www.emailarchitect.net/easendmail/sdk/?ct=protocol
    ' Set Exchange Web Service Protocol - EWS - Exchange 2007/2010/2013/2016
    ' oSmtp.Protocol = 1

	oSmtp.AddRecipient Recipient, Recipient, NORMAL_RECIPIENT

	' | add cc recipient
	' | oSmtp.AddRecipient "test", "test@hotmail.com", COPY_RECIPIENT
	
	' | add bcc recipient
	' | oSmtp.AddRecipient "test", "test@hotmail.com", BLIND_COPY_RECIPIENT
	
	' | -- add file attachment -- |
	' oSmtp.AddAttachment "c:\test.doc" 

	If oSmtp.SendMail() = 0 Then
		WScript.Echo "Sending email to " & Recipient & " succeeded!"
	Else
		WScript.Echo oSmtp.GetLastErrDescription()
	End If

End Sub

WScript.Echo "Please edit this file and input sender, recipient, subject, body and smtp server" 
'Send "dennis@hotmail.com", "support@adminsystem.net", "test subject", "test body", "127.0.0.1"
