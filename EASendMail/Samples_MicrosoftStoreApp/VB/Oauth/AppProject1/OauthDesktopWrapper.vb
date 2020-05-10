Imports System.Net
Imports System.Text
Imports EASendMail

Public Class OauthDesktopWrapper
    Public Sub New(ByVal provider As OauthProvider)
        _provider = provider
    End Sub

    Private _provider As OauthProvider = Nothing

    Public ReadOnly Property Provider As OauthProvider
        Get
            Return _provider
        End Get
    End Property

    Public ReadOnly Property IsAccessTokenExpired As Boolean
        Get
            Return (_provider.AccessTokenTimeStamp.AddSeconds(_provider.TokenExpiresInSeconds - 30) < DateTime.Now)
        End Get
    End Property

    Private _authorizationCode As String = String.Empty
    Public Property AuthorizationCode As String
        Get
            Return _authorizationCode
        End Get
        Set(ByVal value As String)
            _authorizationCode = value
        End Set
    End Property

    Public Async Function RequestAccessTokenAndUserEmailAsync() As Task
        If String.IsNullOrWhiteSpace(AuthorizationCode) Then
            Throw New Exception("Authorization code is not existed!")
        End If

        Dim requestData As String = _provider.TokenRequestData(AuthorizationCode)
        Dim responseText As String = Await _postStringAsync(_provider.TokenUri, requestData)
        Dim parser As OAuthResponseParser = New OAuthResponseParser()
        parser.Load(responseText)
        _provider.AccessToken = parser.AccessToken
        _provider.RefreshToken = parser.RefreshToken
        _provider.UserEmail = parser.EmailInIdToken
        _provider.TokenExpiresInSeconds = parser.TokenExpiresInSeconds

        If String.IsNullOrWhiteSpace(_provider.AccessToken) Then
            Throw New Exception("Failed to request access token!")
        End If

        If String.IsNullOrWhiteSpace(_provider.UserEmail) Then
            Throw New Exception("Failed to request user email address!")
        End If
    End Function

    Public Async Function RefreshAccessTokenAsync() As Task
        If String.IsNullOrWhiteSpace(_provider.RefreshToken) Then
            Throw New Exception("Refresh token is not existed!")
        End If

        Dim requestData As String = _provider.RefreshTokenRequestData()
        Dim responseText As String = Await _postStringAsync(_provider.TokenUri, requestData)
        Dim parser As OAuthResponseParser = New OAuthResponseParser()
        parser.Load(responseText)
        _provider.AccessToken = parser.AccessToken

        If Not String.IsNullOrEmpty(parser.RefreshToken) Then
            _provider.RefreshToken = parser.RefreshToken
        End If

        If parser.TokenExpiresInSeconds > 0 Then
            _provider.TokenExpiresInSeconds = parser.TokenExpiresInSeconds
        End If
    End Function

    Private Async Function _postStringAsync(ByVal uri As String, ByVal requestData As String) As Task(Of String)
        Dim httpRequest As HttpWebRequest = TryCast(WebRequest.Create(uri), HttpWebRequest)
        httpRequest.Method = "POST"
        httpRequest.ContentType = "application/x-www-form-urlencoded"

        Using stream = Await httpRequest.GetRequestStreamAsync()
            Dim buffer = Encoding.UTF8.GetBytes(requestData)
            Await stream.WriteAsync(buffer, 0, buffer.Length)
        End Using

        Dim httpResponse As WebResponse = Await httpRequest.GetResponseAsync()

        Using stream = httpResponse.GetResponseStream()
            Dim buffer = Await ReadStreamAsync(stream, 0)
            Return Encoding.UTF8.GetString(buffer, 0, buffer.Length)
        End Using
    End Function

    Private Async Function ReadStreamAsync(ByVal stream As Stream, ByVal InitSize As Integer) As Task(Of Byte())
        Dim readsize As Integer = 8192

        If InitSize <= 0 Then
            InitSize = readsize * 2
        End If

        Dim bufsize As Integer = InitSize + readsize
        Dim buf As Byte() = New Byte(bufsize + 1 - 1) {}
        Dim read As Integer = 0
        Dim cursize As Integer = 0

        While True
            read = Await stream.ReadAsync(buf, cursize, readsize)
            If read <= 0 Then Exit While
            cursize += read

            If bufsize <= readsize + cursize Then

                If bufsize < 1024 * 1024 Then
                    bufsize = bufsize * 2 + readsize
                Else
                    bufsize = bufsize + 1024 * 1024 + readsize
                End If

                Dim t As Byte() = New Byte(bufsize - 1) {}
                System.Buffer.BlockCopy(buf, 0, t, 0, cursize)
                buf = t
            End If
        End While

        Dim data As Byte() = New Byte(cursize - 1) {}
        Buffer.BlockCopy(buf, 0, data, 0, cursize)
        Return data
    End Function
End Class

