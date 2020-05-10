Imports System.Web.Mvc
Imports EASendMail

Namespace Controllers
    Public Class MyOauthController
        Inherits Controller

        Function Index() As ActionResult
            Dim myTask = _buildDefaultTask()
            Return _mailView(myTask)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Function Index(ByVal myTask As MailTask) As ActionResult
            _setDefaultViewBagValue()

            If ModelState.IsValid Then

                Try
                    Return _doOauth(myTask)
                Catch ep As Exception
                    ViewBag.SyncSendStatus = ep.Message
                End Try

                Return _mailView(myTask)
            End If

            Dim OauthWrapper = _initOauthWrapper(myTask.OauthProvider)
            ViewBag.TokenIsExisted = Not String.IsNullOrEmpty(OauthWrapper.Provider.AccessToken)
            Return _mailView(myTask)
        End Function

        ' Please add http://localhost:55659/oauth/token to Authorized redirect URIs in your Google project.
        ' to learn more detail, please refer to GoogleOauthProvider.cs
        Public Function Token(ByVal code As String, ByVal state As String) As ActionResult
            _setDefaultViewBagValue()
            Dim myTask As MailTask = TempMailTaskStore.GetTask(state)

            If myTask Is Nothing Then
                myTask = _buildDefaultTask()
                ViewBag.SyncSendStatus = "Specified mail task is not found in temporal storage, please try it again."
                Return _mailView(myTask)
            End If

            Dim OauthWrapper = _initOauthWrapper(myTask.OauthProvider)

            Try
                OauthWrapper.AuthorizationCode = code
                OauthWrapper.RequestAccessTokenAndUserEmail()
            Catch ep As Exception
                ViewBag.SyncSendStatus = ep.Message
                Return _mailView(myTask)
            End Try

            If myTask.IsAsyncTask Then
                ViewBag.IsSyncSendSucceeded = True
                ViewBag.SyncSendStatus = "Oauth is completed, ready to send email."
                ViewBag.AutoAsyncSend = True
                ViewBag.TokenIsExisted = Not String.IsNullOrEmpty(OauthWrapper.Provider.AccessToken)
                Return _mailView(myTask)
            End If

            Return _syncSendMail(myTask)
        End Function

        ' Access token is existed, submit email directly from ajax send.
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Function AsyncSend(ByVal myTask As MailTask) As ActionResult
            Dim OauthWrapper = _initOauthWrapper(myTask.OauthProvider)
            If OauthWrapper.IsAccessTokenExpired Then

                Try
                    OauthWrapper.RefreshAccessToken()
                Catch
                    AsyncSendMailService.PutErrorStatus(myTask.TaskId, "Failed to refresh access token.")
                    Return Content(myTask.TaskId)
                End Try
            End If

            myTask.AuthType = SmtpAuthType.XOAUTH2
            myTask.User = OauthWrapper.Provider.UserEmail
            myTask.Password = OauthWrapper.Provider.AccessToken
            myTask.IsAuthenticationRequired = True
            myTask.Sender = OauthWrapper.Provider.UserEmail

            AsyncSendMailService.CreateAsyncTask(myTask)
            Return Content(myTask.TaskId)
        End Function

        Public Function QueryAsyncTask(ByVal id As String) As ActionResult
            Dim status As AsyncSendMailStatus = AsyncSendMailService.QueryStatus(id)

            If status Is Nothing Then
                status = New AsyncSendMailStatus()
                status.TaskId = id
                status.Completed = True
                status.HasError = True
                status.Status = "Invalid Task ID"
            End If

            Return Json(status, JsonRequestBehavior.AllowGet)
        End Function

        Public Function ClearToken() As ActionResult
            Dim OauthWrapper = _initOauthWrapper(OauthProvider.GoogleSmtpProvider)
            OauthWrapper.Provider.ClearToken()
            OauthWrapper.AuthorizationCode = ""
            Return Content("OK")
        End Function

        Private Function _mailView(ByVal myTask As MailTask) As ActionResult
            ViewBag.Port = DropDownListData.PortList(myTask.Port)
            ViewBag.OauthProvider = DropDownListData.OauthList(myTask.OauthProvider)
            Return View("Index", myTask)
        End Function

        Private Sub _setDefaultViewBagValue()
            ViewBag.IsSyncSendSucceeded = False
            ViewBag.SyncSendStatus = String.Empty
            ViewBag.AutoAsyncSend = False
            ViewBag.TokenIsExisted = False
        End Sub

        Private Function _buildDefaultTask() As MailTask
            Dim OauthWrapper = _initOauthWrapper(OauthProvider.GoogleSmtpProvider)
            Dim myTask = New MailTask()
            myTask.Subject = "Test email from ASP.NET MVC with XOAUTH2"
            myTask.IsAuthenticationRequired = True
            myTask.AuthType = SmtpAuthType.XOAUTH2
            myTask.Server = "smtp.gmail.com"
            myTask.Port = 587
            myTask.OauthProvider = OauthProvider.GoogleSmtpProvider
            myTask.IsSslConnection = True
            myTask.Sender = "unspecified"

            Dim body = New StringBuilder()
            body.Append("This sample demonstrates how to send email from ASP.NET MVC with XOAUTH2." & vbCrLf & vbCrLf)
            body.Append("Please apply for your Google/Microsoft client_id and client_secret as introduced in OauthProvider.cs." & vbCrLf)
            body.Append("If you got ""This app isn't verified"" information, please click ""advanced"" -> Go to ... for test." & vbCrLf)
            myTask.TextBody = body.ToString()

            _setDefaultViewBagValue()
            ViewBag.TokenIsExisted = Not String.IsNullOrEmpty(OauthWrapper.Provider.AccessToken)
            Return myTask
        End Function

        Private Function _doOauth(ByVal myTask As MailTask) As ActionResult
            _setDefaultViewBagValue()

            Dim OauthWrapper = _initOauthWrapper(myTask.OauthProvider)
            If Not String.IsNullOrEmpty(OauthWrapper.Provider.AccessToken) Then

                If Not OauthWrapper.IsAccessTokenExpired Then
                    Return _syncSendMail(myTask)
                End If

                Try
                    OauthWrapper.RefreshAccessToken()
                    Return _syncSendMail(myTask)
                Catch
                End Try
            End If

            TempMailTaskStore.PutTask(myTask)
            Return Redirect(OauthWrapper.Provider.GetFullAuthUri() & "&state=" + myTask.TaskId)
        End Function

        Private Function _syncSendMail(ByVal myTask As MailTask) As ActionResult
            Dim OauthWrapper = _initOauthWrapper(myTask.OauthProvider)
            ViewBag.TokenIsExisted = Not String.IsNullOrEmpty(OauthWrapper.Provider.AccessToken)

            Try
                myTask.AuthType = SmtpAuthType.XOAUTH2
                myTask.User = OauthWrapper.Provider.UserEmail
                myTask.Password = OauthWrapper.Provider.AccessToken
                myTask.IsAuthenticationRequired = True

                myTask.Sender = OauthWrapper.Provider.UserEmail
                Dim smtp = New SmtpClient()

                Dim server = myTask.BuildServer()
                Dim mail = myTask.BuildMail()

                smtp.SendMail(server, mail)

                ViewBag.IsSyncSendSucceeded = True
                ViewBag.SyncSendStatus = "Message has been submitted to server successfully."
            Catch ep As Exception
                ViewBag.IsSyncSendSucceeded = False
                ViewBag.SyncSendStatus = ep.Message
            End Try

            myTask.TaskId = Guid.NewGuid().ToString()
            Return _mailView(myTask)
        End Function

        Private Function _initOauthWrapper(ByVal providerType As Integer) As OauthHttpWrapper
            Dim OauthWrapper = TryCast(Session("OauthWrapper"), OauthHttpWrapper)

            If OauthWrapper IsNot Nothing Then
                If OauthWrapper.Provider.ProviderType = providerType Then
                    Return OauthWrapper
                End If
            End If

            Dim provider As OauthProvider = Nothing
            Select Case (providerType)
                Case OauthProvider.MsLiveProvider
                    provider = OauthProvider.CreateMsLiveProvider()
                Case OauthProvider.MsOffice365Provider
                    provider = OauthProvider.CreateMsOffice365Provider()
                Case OauthProvider.GoogleGmailApiProvider
                    provider = OauthProvider.CreateGoogleGmailApiProvider()
                Case Else
                    provider = OauthProvider.CreateGoogleSmtpProvider()

            End Select

            OauthWrapper = New OauthHttpWrapper(provider)
            Session("OauthWrapper") = OauthWrapper
            Return OauthWrapper
        End Function
    End Class
End Namespace