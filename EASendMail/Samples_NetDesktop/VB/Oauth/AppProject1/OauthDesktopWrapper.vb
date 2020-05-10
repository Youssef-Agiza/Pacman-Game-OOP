Imports System.Text
Imports System.IO
Imports System.Net
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

    Public Sub RequestAccessTokenAndUserEmail()
        If String.IsNullOrWhiteSpace(AuthorizationCode) Then
            Throw New Exception("Authorization code is not existed!")
        End If

        Dim requestData As String = _provider.TokenRequestData(AuthorizationCode)
        Dim responseText As String = _postString(_provider.TokenUri, requestData)
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
    End Sub

    Public Sub RefreshAccessToken()
        If String.IsNullOrWhiteSpace(_provider.RefreshToken) Then
            Throw New Exception("Refresh token is not existed!")
        End If

        Dim requestData As String = _provider.RefreshTokenRequestData()
        Dim responseText As String = _postString(_provider.TokenUri, requestData)
        Dim parser As OAuthResponseParser = New OAuthResponseParser()
        parser.Load(responseText)
        _provider.AccessToken = parser.AccessToken

        If Not String.IsNullOrEmpty(parser.RefreshToken) Then
            _provider.RefreshToken = parser.RefreshToken
        End If

        If parser.TokenExpiresInSeconds > 0 Then
            _provider.TokenExpiresInSeconds = parser.TokenExpiresInSeconds
        End If
    End Sub

    Private Function _postString(ByVal uri As String, ByVal requestData As String) As String
        Dim httpRequest As HttpWebRequest = TryCast(WebRequest.Create(uri), HttpWebRequest)
        httpRequest.Method = "POST"
        httpRequest.ContentType = "application/x-www-form-urlencoded"
        Dim requestAsyncResult As IAsyncResult = httpRequest.BeginGetRequestStream(Nothing, Nothing)

        While (Not requestAsyncResult.AsyncWaitHandle.WaitOne(5, False))
            Application.DoEvents()
        End While

        Dim requestStream As Stream = httpRequest.EndGetRequestStream(requestAsyncResult)
        Dim requestBuffer As Byte() = Encoding.UTF8.GetBytes(requestData)
        requestStream.Write(requestBuffer, 0, requestBuffer.Length)
        requestStream.Close()
        Dim responseAsyncResult As IAsyncResult = httpRequest.BeginGetResponse(Nothing, Nothing)

        While (Not responseAsyncResult.AsyncWaitHandle.WaitOne(5, False))
            Application.DoEvents()
        End While

        Dim httpResponse As HttpWebResponse = TryCast(httpRequest.EndGetResponse(responseAsyncResult), HttpWebResponse)
        Dim responseStream As StreamReader = New StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8)
        Return responseStream.ReadToEnd()
    End Function
End Class

