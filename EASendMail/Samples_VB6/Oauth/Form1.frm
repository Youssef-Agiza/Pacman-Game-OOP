VERSION 5.00
Object = "{EAB22AC0-30C1-11CF-A7EB-0000C05BAE0B}#1.1#0"; "ieframe.dll"
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   7335
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   10215
   LinkTopic       =   "Form1"
   ScaleHeight     =   7335
   ScaleWidth      =   10215
   StartUpPosition =   3  'Windows Default
   Begin VB.CheckBox chkUseHttpListener 
      Caption         =   "Use Http Listener"
      Height          =   495
      Left            =   8160
      TabIndex        =   33
      Top             =   840
      Width           =   1695
   End
   Begin VB.ComboBox lstProvider 
      Height          =   315
      Left            =   6840
      Style           =   2  'Dropdown List
      TabIndex        =   32
      Top             =   1440
      Width           =   3015
   End
   Begin VB.CommandButton btnClearToken 
      Caption         =   "Clear Token"
      Enabled         =   0   'False
      Height          =   375
      Left            =   8040
      TabIndex        =   30
      Top             =   6240
      Width           =   1935
   End
   Begin VB.CommandButton btnCancel 
      Caption         =   "Cancel"
      Enabled         =   0   'False
      Height          =   375
      Left            =   6360
      TabIndex        =   29
      Top             =   6240
      Width           =   1575
   End
   Begin VB.Frame Frame1 
      Height          =   1815
      Left            =   6000
      TabIndex        =   24
      Top             =   120
      Width           =   3975
      Begin VB.TextBox textServer 
         Height          =   285
         Left            =   840
         TabIndex        =   25
         Text            =   "smtp.gmail.com"
         Top             =   240
         Width           =   3015
      End
      Begin VB.ComboBox lstPort 
         Height          =   315
         Left            =   840
         Style           =   2  'Dropdown List
         TabIndex        =   27
         Top             =   780
         Width           =   1215
      End
      Begin VB.Label Lable7 
         Caption         =   "Provider"
         Height          =   255
         Left            =   120
         TabIndex        =   31
         Top             =   1320
         Width           =   615
      End
      Begin VB.Label Label6 
         AutoSize        =   -1  'True
         Caption         =   "Server"
         Height          =   195
         Left            =   120
         TabIndex        =   26
         Top             =   285
         Width           =   465
      End
      Begin VB.Label Label5 
         AutoSize        =   -1  'True
         Caption         =   "Port"
         Height          =   195
         Left            =   120
         TabIndex        =   28
         Top             =   840
         Width           =   285
      End
   End
   Begin SHDocVwCtl.WebBrowser htmlEditor 
      Height          =   2895
      Left            =   120
      TabIndex        =   15
      Top             =   3240
      Width           =   9855
      ExtentX         =   17383
      ExtentY         =   5106
      ViewMode        =   0
      Offline         =   0
      Silent          =   0
      RegisterAsBrowser=   0
      RegisterAsDropTarget=   1
      AutoArrange     =   0   'False
      NoClientEdge    =   0   'False
      AlignLeft       =   0   'False
      NoWebView       =   0   'False
      HideFileNames   =   0   'False
      SingleClick     =   0   'False
      SingleSelection =   0   'False
      NoFolders       =   0   'False
      Transparent     =   0   'False
      ViewID          =   "{0057D0E0-3573-11CF-AE69-08002B2E1262}"
      Location        =   "http:///"
   End
   Begin VB.CommandButton btnInsert 
      Caption         =   "Insert Picture"
      Height          =   315
      Left            =   5760
      TabIndex        =   14
      Top             =   2760
      Width           =   2055
   End
   Begin VB.CommandButton btnC 
      BackColor       =   &H80000005&
      Caption         =   "C"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Left            =   5160
      TabIndex        =   13
      ToolTipText     =   "Choose font color"
      Top             =   2760
      Width           =   375
   End
   Begin VB.CommandButton btnU 
      Caption         =   "U"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   -1  'True
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Left            =   4800
      TabIndex        =   12
      ToolTipText     =   "Underline"
      Top             =   2760
      Width           =   375
   End
   Begin VB.CommandButton btnI 
      Caption         =   "I"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Left            =   4440
      TabIndex        =   11
      ToolTipText     =   "Italic"
      Top             =   2760
      Width           =   375
   End
   Begin VB.CommandButton btnB 
      Caption         =   "B"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Left            =   4080
      TabIndex        =   10
      ToolTipText     =   "Bold"
      Top             =   2760
      Width           =   375
   End
   Begin VB.ComboBox lstSize 
      Height          =   315
      Left            =   2520
      Style           =   2  'Dropdown List
      TabIndex        =   9
      Top             =   2760
      Width           =   1455
   End
   Begin VB.ComboBox lstFonts 
      Height          =   315
      Left            =   120
      Style           =   2  'Dropdown List
      TabIndex        =   8
      Top             =   2760
      Width           =   2295
   End
   Begin MSComctlLib.StatusBar textStatus 
      Align           =   2  'Align Bottom
      Height          =   390
      Left            =   0
      TabIndex        =   23
      Top             =   6945
      Width           =   10215
      _ExtentX        =   18018
      _ExtentY        =   688
      Style           =   1
      _Version        =   393216
      BeginProperty Panels {8E3867A5-8586-11D1-B16A-00C0F0283628} 
         NumPanels       =   1
         BeginProperty Panel1 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.ProgressBar pgBar 
      Height          =   135
      Left            =   120
      TabIndex        =   16
      Top             =   6360
      Width           =   3975
      _ExtentX        =   7011
      _ExtentY        =   238
      _Version        =   393216
      Appearance      =   1
   End
   Begin MSComDlg.CommonDialog dlgFile 
      Left            =   8400
      Top             =   2760
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.CommandButton btnSend 
      Caption         =   "Send"
      Height          =   375
      Left            =   4680
      TabIndex        =   17
      Top             =   6240
      Width           =   1575
   End
   Begin VB.CommandButton btnClear 
      Caption         =   "Clear"
      Height          =   375
      Left            =   9240
      TabIndex        =   7
      Top             =   2235
      Width           =   615
   End
   Begin VB.CommandButton btnAdd 
      Caption         =   "Add"
      Height          =   375
      Left            =   8520
      TabIndex        =   6
      Top             =   2235
      Width           =   615
   End
   Begin VB.TextBox textAttachments 
      BackColor       =   &H80000018&
      Enabled         =   0   'False
      Height          =   285
      Left            =   1200
      TabIndex        =   5
      Top             =   2280
      Width           =   6975
   End
   Begin VB.TextBox textSubject 
      Height          =   285
      Left            =   1080
      TabIndex        =   4
      Text            =   "OAUTH 2.0 Test"
      Top             =   1680
      Width           =   4335
   End
   Begin VB.TextBox textCc 
      Height          =   285
      Left            =   1080
      TabIndex        =   3
      Top             =   1320
      Width           =   4335
   End
   Begin VB.TextBox textTo 
      Height          =   285
      Left            =   1080
      TabIndex        =   2
      Top             =   840
      Width           =   4335
   End
   Begin VB.TextBox textFrom 
      Height          =   285
      Left            =   1080
      TabIndex        =   1
      Top             =   240
      Width           =   4335
   End
   Begin VB.Label Label10 
      AutoSize        =   -1  'True
      Caption         =   "Please separate multiple email addresses with comma(,)"
      Height          =   195
      Left            =   1080
      TabIndex        =   22
      Top             =   600
      Width           =   3900
   End
   Begin VB.Label Label9 
      AutoSize        =   -1  'True
      Caption         =   "Attachments"
      Height          =   195
      Left            =   120
      TabIndex        =   21
      Top             =   2325
      Width           =   885
   End
   Begin VB.Label Label4 
      AutoSize        =   -1  'True
      Caption         =   "Subject"
      Height          =   195
      Left            =   120
      TabIndex        =   20
      Top             =   1680
      Width           =   540
   End
   Begin VB.Label Label3 
      AutoSize        =   -1  'True
      Caption         =   "Cc"
      Height          =   285
      Left            =   120
      TabIndex        =   19
      Top             =   1320
      Width           =   195
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      Caption         =   "To"
      Height          =   195
      Left            =   120
      TabIndex        =   18
      Top             =   885
      Width           =   195
   End
   Begin VB.Label Label2 
      AutoSize        =   -1  'True
      Caption         =   "From"
      Height          =   195
      Left            =   120
      TabIndex        =   0
      Top             =   285
      Width           =   345
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  ===============================================================================
' |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
' |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
' |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
' |    PARTICULAR PURPOSE.                                                    |
' |    Copyright (c)2010 - 2019  ADMINSYSTEM SOFTWARE LIMITED                        |
' |
' |    Project: It demonstrates how to use EASendMail to send email with asynchronous mode + html + s/mime
' |
' |    Author: Ivan Lui (ivan@emailarchitect.net)
'  ===============================================================================

Option Explicit

Public Oauth As New OauthWrapper

Const ConnectNormal = 0
Const ConnectSSLAuto = 1
Const ConnectSTARTTLS = 2
Const ConnectDirectSSL = 3
Const ConnectTryTLS = 4

Const CRYPT_MACHINE_KEYSET = 32
Const CRYPT_USER_KEYSET = 4096
Const CERT_SYSTEM_STORE_CURRENT_USER = 65536
Const CERT_SYSTEM_STORE_LOCAL_MACHINE = 131072

Private m_arCharset(27, 1) As String
Private m_arAttachment() As String

Private WithEvents m_oSmtp As EASendMailObjLib.Mail 'declare EASendMail object
Attribute m_oSmtp.VB_VarHelpID = -1
Private m_bError As Boolean 'this variable indicates if error occurs when sending email
Private m_bCancel As Boolean
Private m_bIdle As Boolean
Private m_lastErrDescription As String

'========================================================
' fnParseAddr
'========================================================
Function fnParseAddr(src, ByRef name, ByRef addr)
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
Function fnTrim(ByRef src, trimer)
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
Function strpbrk(src, start, sCharset)
    strpbrk = 0
    
    Dim i, size, pos, ch
    
    size = Len(src)
    For i = start To size
        ch = Mid(src, i, 1)
        
        If InStr(1, sCharset, ch) >= 1 Then
            strpbrk = i
            Exit Function
        End If
    Next

End Function

'========================================================
' SplitEx
'========================================================
Function SplitEx(src, sCharset)

    Dim find, start, pos, bquoted, ch, item, s
    Dim arItems()
    Dim nItems, nBuf, nIndex
    
    nIndex = 0
    nBuf = 10
    
    ReDim Preserve arItems(nBuf)
    
    find = sCharset & """"
    start = 1
    pos = 1
    bquoted = False
        
    Do While True
        pos = strpbrk(src, pos, find)
        
        If pos <= 0 Then
            s = Mid(src, start)
            fnTrim s, sCharset
            If s <> "" Then
                If nIndex >= nBuf - 1 Then
                    nBuf = nBuf + 10
                    ReDim Preserve arItems(nBuf)
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
            fnTrim s, sCharset
            If s <> "" Then
                If nIndex >= nBuf - 1 Then
                    nBuf = nBuf + 10
                    ReDim Preserve arItems(nBuf)
                End If
                arItems(nIndex) = s
                nIndex = nIndex + 1
            End If
            pos = pos + 1
            start = pos
        Else
            pos = pos + 1
        End If
    Loop
    
    ReDim Preserve arItems(nIndex)
    SplitEx = arItems
End Function

Private Sub InitFonts()
    Dim arFonts
    'you can add more fonts in this array
    arFonts = Array("Choose Font Style ...", _
                    "Arial", _
                    "Arial Baltic", _
                    "Arial Black", _
                    "Basemic Symbol", _
                    "Bookman Old Style", _
                    "Comic Sans MS", _
                    "Courier", _
                    "Courier New", _
                    "Microsoft Sans Serif", _
                    "MS Sans Serif", _
                    "Times New Roman", _
                    "Verdana")
                                            
    Dim i, nCount As Integer
    nCount = UBound(arFonts)
    For i = LBound(arFonts) To nCount
        lstFonts.AddItem arFonts(i), i
    Next
    lstFonts.ListIndex = 0
    
    lstSize.AddItem "Font Size ...", 0
    For i = 1 To 7
        lstSize.AddItem i, i
    Next
    lstSize.ListIndex = 0

End Sub


Private Sub btnAdd_Click()
    dlgFile.Filter = "All files(*.*) | *.*"
    dlgFile.ShowOpen
    If dlgFile.fileName <> vbNullString And dlgFile.fileName <> "" Then
        Dim count As Integer
        count = UBound(m_arAttachment)
        m_arAttachment(count) = dlgFile.fileName
        ReDim Preserve m_arAttachment(count + 1)
        
        Dim pos As Integer
        Dim fileName As String
        fileName = dlgFile.fileName
        pos = InStrRev(fileName, "\")
        If pos > 0 Then
            fileName = Mid(fileName, pos + 1)
        End If
        
        textAttachments.Text = textAttachments.Text & fileName & "; "
    End If
End Sub

Private Sub btnC_Click()
    On Error Resume Next
    dlgFile.CancelError = True
    dlgFile.ShowColor

    If Err.Number = cdlCancel Then
        Exit Sub
    End If
    
    Dim clr As OLE_COLOR
    Dim v, r, g, b
    
    clr = dlgFile.Color
    r = Hex$(clr And &HFF&)
    g = Hex$((clr And &HFF00&) \ &H100&)
    b = Hex$((clr And &HFF0000) \ &H10000)
    If Len(r) = 0 Then
        r = "00"
    ElseIf Len(r) = 1 Then
        r = "0" & r
    End If
    
    If Len(g) = 0 Then
        g = "00"
    ElseIf Len(g) = 1 Then
        g = "0" & g
    End If
    
    If Len(b) = 0 Then
        b = "00"
    ElseIf Len(b) = 1 Then
        b = "0" & b
    End If
   
    v = "#" & r & g & b
    htmlEditor.Document.execCommand "ForeColor", False, v
    htmlEditor.SetFocus
End Sub

Private Sub btnCancel_Click()
    m_oSmtp.Terminate
    m_bCancel = True
    m_bIdle = True
    btnCancel.Enabled = False
End Sub

Private Sub btnClear_Click()
    ReDim m_arAttachment(0)
    textAttachments.Text = ""
End Sub

Private Sub btnClearToken_Click()
    Oauth.Clear
    btnClearToken.Enabled = False
End Sub

Private Sub chkUseHttpListener_Click()
    btnClearToken_Click
End Sub

Private Sub btnInsert_Click()
    htmlEditor.Document.execCommand "InsertImage", True
End Sub

Private Function DoOauth() As Boolean

    DoOauth = False
    
    If Oauth.AccessToken <> "" Then
        If Not Oauth.AccessTokenIsExpired() Then
           DoOauth = True
           Exit Function
        End If
        
        textStatus.SimpleText = "Refresh expired access token from server ..."
        If Oauth.RefreshAccessToken() Then
            DoOauth = True
            Exit Function
        End If
        
        Oauth.ClearError
        textStatus.SimpleText = "Failed to refresh expired access token, now request new token again"
    End If
    
    Oauth.UseHttpListener = chkUseHttpListener.Value
    FormOauth.Show 1, Me
    If Oauth.AuthorizationCode = "" Then
        MsgBox "Failed to login user and get authorization code."
        Exit Function
    End If
    
    textStatus.SimpleText = "Request access token from server ..."
    If Not Oauth.RequestAccessTokenAndUserEmail() Then
        MsgBox Oauth.GetLastError()
        Exit Function
    End If
    
    DoOauth = True
    
End Function

Private Sub btnSend_Click()

    btnSend.Enabled = False
    If Not DoOauth() Then
        textStatus.SimpleText = "Failed to request/refresh access token."
        btnSend.Enabled = True
        Exit Sub
    End If
    
    btnSend.Enabled = True
    textStatus.SimpleText = "Oauth is completed."
    
    If Trim(textTo.Text) = "" And _
        Trim(textCc.Text) = "" Then
            MsgBox ("Please input To or Cc, the format can be test@adminsystem.com or Tester<test@adminsystem.com>, please use comma(,) to separate multiple addresses")
            Exit Sub
    End If
    
    btnSend.Enabled = False
    btnCancel.Enabled = True
    btnClearToken.Enabled = False
    
    ' because m_oSmtp is a member variahle, so we need to clear the the property
    m_oSmtp.Reset
    m_oSmtp.Charset = "utf-8"

    m_oSmtp.Asynchronous = 1
    m_oSmtp.ServerAddr = Trim(textServer.Text)
    m_oSmtp.ServerPort = lstPort.Text

    m_oSmtp.Protocol = 0 ' SMTP Protocol
    
    If lstProvider.ListIndex = 2 Then
        m_oSmtp.Protocol = 1 ' EWS Protocol
    ElseIf lstProvider.ListIndex = 3 Then
        m_oSmtp.Protocol = 3 ' Gmail Api Protocol
    End If
    
    m_oSmtp.AuthType = 5 ' XOAUTH2 type
    m_oSmtp.UserName = Oauth.UserEmail
    m_oSmtp.Password = Oauth.AccessToken
    
    ' Use SSL/TLS based on server port.
    m_oSmtp.ConnectType = ConnectSSLAuto
    
    m_oSmtp.From = Oauth.UserEmail
    Dim name, addr As String
    fnParseAddr Trim(textFrom.Text), name, addr
    
    If addr <> "" And LCase(addr) <> Oauth.UserEmail Then
        m_oSmtp.ReplyTo = addr
    End If
    
    Dim to_addr, cc_addr As String
     
    to_addr = Trim(textTo.Text)
    cc_addr = Trim(textCc.Text)
    
    m_oSmtp.AddRecipientEx to_addr, 0  ' 0, Normal recipient, 1, cc, 2, bcc
    m_oSmtp.AddRecipientEx cc_addr, 0
    
    Dim recipients As String
    recipients = to_addr & "," & cc_addr
    fnTrim recipients, ","
    
    Dim i, count As Integer
   
    count = UBound(m_arAttachment)
    For i = 0 To count - 1
        If m_oSmtp.AddAttachment(m_arAttachment(i)) <> 0 Then
            MsgBox m_oSmtp.GetLastErrDescription() & ":" & m_arAttachment(i)
            btnSend.Enabled = True
            btnCancel.Enabled = False
            Exit Sub
        End If
    Next
    
    Dim Subject
    Subject = textSubject.Text
    
    m_oSmtp.BodyFormat = 1    ' Using HTML FORMAT to send mail
    m_oSmtp.AltBody = htmlEditor.Document.body.innerText
    Dim html As String
    html = htmlEditor.Document.body.innerHTML
    
    Dim header As String
    header = "<html><title>" & Subject & "</title><meta HTTP-EQUIV=""Content-Type"" Content=""text-html; charset=" & m_oSmtp.Charset & """><META content=""MSHTML 6.00.2900.2769"" name=GENERATOR><body>"
    'imports html with embedded pictures
    html = header & html
    html = html & "</body></html>"
    m_oSmtp.ImportHtml html, App.Path
    
    m_oSmtp.Subject = Subject
    
    textStatus.SimpleText = "Connecting " & m_oSmtp.ServerAddr & " ..."
    m_bIdle = False
    m_bCancel = False
    m_bError = False
    pgBar.Value = 0
    
    m_oSmtp.SendMail
    
    'wait the asynchronous call finish.
    Do While Not m_bIdle
        DoEvents
    Loop
    
    If m_bCancel Then
        textStatus.SimpleText = "Operation is canceled by user."
    ElseIf m_bError Then
        textStatus.SimpleText = m_lastErrDescription
    Else
        textStatus.SimpleText = "Message was delivered successfully"
    End If
    
    MsgBox textStatus.SimpleText
    btnSend.Enabled = True
    btnCancel.Enabled = False
    btnClearToken.Enabled = (Oauth.AccessToken <> "")
End Sub


Private Sub Form_Load()

    lstPort.AddItem "25"
    lstPort.AddItem "587"
    lstPort.AddItem "465"
    lstPort.ListIndex = 1
    
    lstProvider.AddItem "Gmail OAUTH + SMTP"
    lstProvider.AddItem "Live OAUTH + SMTP (Hotmail)"
    lstProvider.AddItem "EWS OAUTH (Office365)"
    lstProvider.AddItem "Gmail API + OAUTH"
    lstProvider.ListIndex = 0
    
    InitFonts
    htmlEditor.Navigate2 "about:blank"
    htmlEditor.Document.designMode = "on"
    
    ReDim m_arAttachment(0)

    'Declare and create easendmail mail object instance
    Set m_oSmtp = New EASendMailObjLib.Mail
    'The license code for EASendMail ActiveX Object,
    'for evaluation usage, please use "TryIt" as the license code.
    m_oSmtp.LicenseCode = "TryIt"
    'm_oSmtp.LogFileName = App.Path & "\smtp.txt" 'enable smtp log
    
End Sub

Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer)
    End
End Sub

Private Sub Form_Resize()
    On Error Resume Next
    If (Me.Width < 10200) Then
        Me.Width = 10200
    End If

    If (Me.Height < 7500) Then
        Me.Height = 7500
    End If
    
    htmlEditor.Width = Me.Width - 450
    htmlEditor.Height = Me.Height - 4800

    btnSend.Top = htmlEditor.Height + htmlEditor.Top + 150
    btnSend.Left = Me.Width - 5600
    btnCancel.Top = btnSend.Top
    btnCancel.Left = btnSend.Left + btnSend.Width + 100
    btnClearToken.Top = btnSend.Top
    btnClearToken.Left = btnCancel.Left + btnCancel.Width + 100
    
    pgBar.Top = btnSend.Top + 100
    pgBar.Width = Me.Width - 6000
    
    Frame1.Left = Me.Width - 4400

    textFrom.Width = Me.Width - 5800
    textTo.Width = textFrom.Width
    textCc.Width = textFrom.Width
    textSubject.Width = textFrom.Width

    textAttachments.Width = Me.Width - 2900
    btnAdd.Left = textAttachments.Width + 1200
    btnClear.Left = textAttachments.Width + 1900
End Sub

Private Sub htmlEditor_NavigateComplete2(ByVal pDisp As Object, URL As Variant)
    Dim s As String
    s = s & "<div>This sample demonstrates how to send html email using Gmail/MS Live/MS Office365 OAUTH 2.0.</div><div>&nbsp;</div>"
    s = s & "<div>Please create your client_id by Google/Ms developer console, you can find the introduction in source codes.</div>"
    s = s & "<div>If you got ""This app isn't verified"" information, please click ""advanced"" -> Go to ... for test.</div>"
    s = s & "<div>&nbsp;</div>"

    htmlEditor.Document.body.innerHTML = s
End Sub

Private Sub lstFonts_Click()
    If lstFonts.ListIndex = 0 Then
        Exit Sub
    End If
    htmlEditor.Document.execCommand "fontname", False, lstFonts.Text
    lstFonts.ListIndex = 0
    htmlEditor.SetFocus
End Sub

Private Sub lstProvider_Click()
    Select Case lstProvider.ListIndex
    Case 0
        textServer.Text = "smtp.gmail.com"
        lstPort.Enabled = True
        Oauth.InitGoogleSmtpProvider
    Case 1
        textServer.Text = "smtp.live.com"
        lstPort.Enabled = True
        Oauth.InitMsLiveProvider
    Case 2
        textServer.Text = "outlook.office365.com"
        lstPort.Enabled = False
        Oauth.InitMsOffice365Provider
    Case 3
        textServer.Text = "https://www.googleapis.com/upload/gmail/v1/users/me/messages/send?uploadType=media"
        lstPort.Enabled = False
        Oauth.InitGoogleGmailApiProvider
    End Select
End Sub

Private Sub lstSize_click()
    If lstSize.ListIndex = 0 Then
        Exit Sub
    End If
    htmlEditor.Document.execCommand "fontsize", False, lstSize.Text
    lstSize.ListIndex = 0
    htmlEditor.SetFocus
End Sub

Private Sub btnU_Click()
    htmlEditor.Document.execCommand "underline", False, Nothing
    htmlEditor.SetFocus
End Sub

Private Sub btnB_Click()
    htmlEditor.Document.execCommand "Bold", False, Nothing
      
    htmlEditor.SetFocus
End Sub

Private Sub btnI_Click()
    htmlEditor.Document.execCommand "Italic", False, Nothing
    htmlEditor.SetFocus
End Sub

'=====================================================
' Method Object_OnAuthenticated
' Type: Event, fired when ESMTP user authentication is successful
' Parameter: nothing
' Return value: nothing
'=====================================================
Private Sub m_oSmtp_OnAuthenticated()
    textStatus.SimpleText = "Authorized"
End Sub

'=====================================================
' Method Object_OnClosed
' Type: Event, fired when server disconncts with client
' Parameter: nothing
' Return value: nothing
'=====================================================
Private Sub m_oSmtp_OnClosed()
    m_bIdle = True
End Sub

'=====================================================
' Method Object_OnConnected
' Type: Event, fired when client successfully connects server
' Parameter: nothing
' Return value: nothing
'=====================================================
Private Sub m_oSmtp_OnConnected()
    textStatus.SimpleText = "Connected"
End Sub

'======================================================
' Method Object_OnError
' Type: Event, fired when an error occurs when sending email
' Parameter:
'     lError: error code, refer to EASendMail documentation
'     ErrDescription: error description
' Return value: nothing
'======================================================
Private Sub m_oSmtp_OnError(ByVal lError As Long, ByVal ErrDescription As String)
    m_bError = True
    m_lastErrDescription = ErrDescription
    m_bIdle = True
End Sub

'=======================================================
' Method Object_OnSending
' Type: Event, fired when EASendMail object is sending an email body
' Parameter:
'     lSent: size of sent data in bytes
'     lTotal: size of email body in bytes
' Return value: nothing
'=======================================================
Private Sub m_oSmtp_OnSending(ByVal lSent As Long, ByVal lTotal As Long)
    If lSent = 0 Then
        textStatus.SimpleText = "Sending...."
        pgBar.Min = 0
        pgBar.Max = lTotal
        pgBar.Value = 0
    ElseIf lSent = lTotal Then
        textStatus.SimpleText = "Disconnecting..."
        pgBar.Value = lTotal
    Else
        pgBar.Value = lSent
    End If
End Sub
