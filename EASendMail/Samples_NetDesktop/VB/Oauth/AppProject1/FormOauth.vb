'       Because Web Browser control is used for OAUTH, 
'       Web browser control uses IE7 rendering mode by default, 
'       it doesn't support latest Google Web Login Page.

'       You should install IE 10/IE11 (recommended) or later version on your current machine,
'       and then add/mergin the following registry values to use IE 10 mode. 

'       "AppProject1.exe" is your executable file name.
'       In current project, it is "AppProject1.exe"
'       If you debug it in VS, please also add "AppProject1.vshost.exe"

'       Windows Registry Editor Version 5.00 
'       [HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION] 
'       "AppProject1.exe"=dword:00002AF9
'       "AppProject1.vshost.exe"=dword:00002AF9

'       [HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION]
'       "AppProject1.exe"=dword:00002AF9
'       "AppProject1.vshost.exe"=dword:00002AF9

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

Imports System.Text
Imports System.Net
Imports System.Net.Sockets

Public Class FormOauth
    Public Property OauthWrapper As OauthDesktopWrapper = Nothing
    Private _httpListener As HttpListener = Nothing

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.DialogResult = DialogResult.Cancel
    End Sub

    ' if HttpListener is not used, you can remove async
    Private Async Sub FormOauth_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        OauthWrapper.AuthorizationCode = ""
        If Not OauthWrapper.Provider.UseHttpListener Then
            OauthWrapper.Provider.ResetLocalRedirectUri()
            OauthBrowser.Navigate(OauthWrapper.Provider.GetFullAuthUri())
            Return
        End If

        ' Http Listener is Google recommended solution for desktop app, 
        ' and MS OAUTH supports it as well, but you need to add http://127.0.0.1 to 
        ' Azure portal -> Your app -> Authentication -> Mobile and desktop applications: redirect Uri, please check the following URI.
        Dim httpRedirectUri As String = GetHttpRedirectUri()
        OauthWrapper.Provider.RedirectUri = httpRedirectUri
        OauthBrowser.Navigate(OauthWrapper.Provider.GetFullAuthUri())

        Try
            Await GetCodefromHttpListener(httpRedirectUri)
        Catch ep As Exception
            MessageBox.Show(ep.Message)
        Finally
            Me.Close()
        End Try
    End Sub

    Private Async Function GetCodefromHttpListener(ByVal httpRedirectUri As String) As Task
        _httpListener = New HttpListener()
        _httpListener.Prefixes.Add(httpRedirectUri)
        _httpListener.Start()

        Try
            Dim context = Await _httpListener.GetContextAsync()

            Dim responseString As String = String.Format("<html><head><title>Authorization is completed</title></head><body>AuthorizationCode is returned, please close current window and return to the app.</body></html>")

            Dim buffer = Encoding.UTF8.GetBytes(responseString)
            Dim response = context.Response
            response.ContentLength64 = buffer.Length

            Dim responseOutput = response.OutputStream
            Await responseOutput.WriteAsync(buffer, 0, buffer.Length)
            responseOutput.Close()
            _httpListener.[Stop]()

            OauthBrowser.[Stop]()

            If context.Request.QueryString.[Get]("error") IsNot Nothing Then
                MessageBox.Show("OAuth authorization error: {0}.", context.Request.QueryString.[Get]("error"))
                Return
            End If

            Dim code = context.Request.QueryString.[Get]("code")

            OauthWrapper.AuthorizationCode = code
        Catch __unusedObjectDisposedException1__ As ObjectDisposedException
            ' this exception is thrown by closing window, don't handle it.
        End Try
    End Function

    Private Shared Function GetHttpRedirectUri() As String
        Dim listener = New TcpListener(IPAddress.Loopback, 0)
        listener.Start()
        Dim port = (CType(listener.LocalEndpoint, IPEndPoint)).Port
        listener.[Stop]()
        Return String.Format("http://127.0.0.1:{0}/", port)
    End Function

    Private Sub FormOauth_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If _httpListener IsNot Nothing Then
            _httpListener.Close()
            _httpListener = Nothing
        End If
    End Sub

    Private Sub OauthBrowser_DocumentCompleted(ByVal sender As Object, ByVal e As WebBrowserDocumentCompletedEventArgs) Handles OauthBrowser.DocumentCompleted
        If OauthWrapper.AuthorizationCode.Length > 0 Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If

        If Not OauthWrapper.Provider.ParseAuthorizationCodeInHtml Then
            Return
        End If

        Dim elment As HtmlElement = OauthBrowser.Document.GetElementById("code")
        If elment Is Nothing Then
            Return
        End If

        OauthWrapper.AuthorizationCode = elment.GetAttribute("value")

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub OauthBrowser_DocumentTitleChanged(sender As Object, e As EventArgs) Handles OauthBrowser.DocumentTitleChanged
        Me.Text = OauthBrowser.DocumentTitle
    End Sub

    Private Sub OauthBrowser_Navigated(sender As Object, e As WebBrowserNavigatedEventArgs) Handles OauthBrowser.Navigated

        Dim code As String = _parseCodeFromUri(e.Url.Query, "approvalCode=")

        If code.Length = 0 Then
            code = _parseCodeFromUri(e.Url.Query, "code=")
        End If

        If code.Length = 0 Then
            Return
        End If

        ' Close form in OauthBrowser_DocumentCompleted event instead of here
        ' If close form here, Google OAUTH Page may open a default web browser window.
        OauthWrapper.AuthorizationCode = code

    End Sub

    Private Shared Function _parseCodeFromUri(ByVal input As String, ByVal key As String) As String
        Dim pos As Integer = input.IndexOf("?"c)

        If pos <> -1 Then
            input = input.Substring(pos + 1)
        End If

        Dim parameters As String() = input.Split(New Char() {"&"c})

        For i As Integer = 0 To parameters.Length - 1
            Dim parameter As String = parameters(i)

            If String.Compare(parameter, 0, key, 0, key.Length, True) = 0 Then
                Return parameter.Substring(key.Length)
            End If
        Next

        Return String.Empty
    End Function


End Class