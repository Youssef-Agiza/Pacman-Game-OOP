VERSION 5.00
Object = "{EAB22AC0-30C1-11CF-A7EB-0000C05BAE0B}#1.1#0"; "ieframe.dll"
Begin VB.Form FormOauth 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Gmail Web Login"
   ClientHeight    =   11040
   ClientLeft      =   2760
   ClientTop       =   3750
   ClientWidth     =   7710
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   11040
   ScaleWidth      =   7710
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin SHDocVwCtl.WebBrowser OauthBrowser 
      Height          =   10575
      Left            =   120
      TabIndex        =   0
      Top             =   240
      Width           =   7455
      ExtentX         =   13150
      ExtentY         =   18653
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
End
Attribute VB_Name = "FormOauth"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Option Explicit

'       Because Web Browser control is used for OAUTH, 
'       Web browser control uses IE7 rendering mode by default, 
'       it doesn't support latest Google Web Login Page.

'       You should install IE 10/IE11 (recommended) or later version on your current machine,
'       and then add/mergin the following registry values to use IE 10 mode. 

'       "Project1.exe" is your executable file name.
'       In current project, it is "Project1.exe"
'       If you debug it in VS, please also add "Project1.vshost.exe"

'       Windows Registry Editor Version 5.00 
'       [HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION] 
'       "Project1.exe"=dword:00002AF9
'       "Project1.vshost.exe"=dword:00002AF9

'       [HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION]
'       "Project1.exe"=dword:00002AF9
'       "Project1.vshost.exe"=dword:00002AF9

'       Appendix - Web Browser Control Mode:

'       11001 (0x2AF9) Internet Explorer 11. Webpages are displayed in IE11 Standards mode, regardless of the !DOCTYPE directive. 
'       11000 (0x2AF8) Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 mode. 
'       10001 (0x2711) Internet Explorer 10. Webpages are displayed in IE10 Standards mode, regardless of the !DOCTYPE directive. 
'       10000 (0x2710) Internet Explorer 10. Webpages containing standards-based !DOCTYPE directives are displayed in IE10 mode. 
'       9999 (0x270F) Internet Explorer 9. Webpages are displayed in IE9 Standards mode, regardless of the !DOCTYPE directive. 
'       9000 (0x2328) Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode. 
'       8888 (0x22B8) Webpages are displayed in IE8 Standards mode, regardless of the !DOCTYPE directive. 
'       8000 (0x1F40) Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode. 
'       7000 (0x1B58) Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode. This mode is kind of pointless since it's the default.

Private WithEvents oHttpListener As HttpListener
Attribute oHttpListener.VB_VarHelpID = -1

Private Sub Form_Load()
    Dim authPath As String
    OauthBrowser.Silent = True

    If Not Form1.Oauth.UseHttpListener Then
        Form1.Oauth.ResetLocalRedirectUri
        authPath = Form1.Oauth.GetFullAuthUri()
        OauthBrowser.Object.Navigate authPath
        Exit Sub
    End If
    
    ' Http Listener is Google recommended solution for desktop app, 
    ' and MS OAUTH supports it as well, but you need to add http://127.0.0.1 to 
    ' Azure portal -> Your app -> Authentication -> Mobile and desktop applications: redirect Uri, please check the following URI.
    Set oHttpListener = New HttpListener
    If Not oHttpListener.Create("127.0.0.1", 0) Or _
        Not oHttpListener.BeginGetRequestUrl() Then
    
        MsgBox oHttpListener.GetLastError()
        Unload Me
    End If
    
    Form1.Oauth.RedirectUri = "http://127.0.0.1:" & oHttpListener.ListenPort
    authPath = Form1.Oauth.GetFullAuthUri()
    OauthBrowser.Object.Navigate authPath
    
End Sub


Private Sub OauthBrowser_DocumentComplete(ByVal pDisp As Object, URL As Variant)
    
    If Not Form1.Oauth.ParseAuthorizationCodeInHtml Then
        Exit Sub
    End If
    
    Dim htmlDoc As HTMLDocument
    Set htmlDoc = OauthBrowser.Object.Document
    
    Dim htmlInput As HTMLInputElement
    Set htmlInput = htmlDoc.getElementById("code")
    
    If htmlInput Is Nothing Then
        Exit Sub
    End If
    
    Dim code As String
    code = htmlInput.Value
    
    If code <> "" Then
        Form1.Oauth.AuthorizationCode = code
        Unload Me
    End If

End Sub

Private Sub OauthBrowser_NavigateComplete2(ByVal pDisp As Object, URL As Variant)
    
    Dim code As String
    code = ParseCode(URL, "code=")
    If code <> "" Then
        Form1.Oauth.AuthorizationCode = code
        Unload Me
    End If

    code = ParseCode(URL, "approvalCode=")
    If code <> "" Then
        Form1.Oauth.AuthorizationCode = code
        Unload Me
    End If
    
End Sub

Private Function ParseCode(URL As Variant, ParameterName As String) As String

    ParseCode = ""
    Dim Uri As String
    Uri = URL
    
    Dim pos As Integer
    pos = InStr(1, Uri, "?")
    If pos <= 0 Then
        Exit Function
    End If
    
    Uri = Mid(URL, pos + 1)
    Dim parameters, i, uriParameter
    parameters = Split(Uri, "&")
    
    For i = LBound(parameters) To UBound(parameters)
        uriParameter = parameters(i)
        If InStr(1, uriParameter, ParameterName, vbTextCompare) = 1 Then
            ParseCode = Mid(uriParameter, Len(ParameterName) + 1)
            Exit Function
        End If
    Next
End Function

Private Sub OauthBrowser_TitleChange(ByVal Text As String)
    Me.Caption = Text
End Sub

Private Sub oHttpListener_OnError(ByVal oSender As Object, ByVal ErrorDescription As String)
    If Form1.Oauth.AuthorizationCode <> "" Then
        MsgBox ErrorDescription
    End If
    Unload Me
End Sub

Private Sub oHttpListener_OnRequest(ByVal oSender As Object, ByVal URL As String)
    Dim code As String
    
    code = ParseCode(URL, "code=")
    If code = "" Then
        oHttpListener.SendResponse 200, "text/html; charset=utf8", "Authorization code is not return, please close window and retry"
        oHttpListener.Close
        
        Dim error As String
        error = ParseCode(URL, "error=")
        MsgBox "Error with request url " & error
        Unload Me
        Exit Sub
    End If

    oHttpListener.SendResponse 200, "text/html; charset=utf8", "Authorization code is returned, please close window and return to your app"
    OauthBrowser.Stop
    
    Form1.Oauth.AuthorizationCode = code
    Unload Me
End Sub

Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer)
    
    If Not oHttpListener Is Nothing Then
        oHttpListener.Close
    End If
End Sub

