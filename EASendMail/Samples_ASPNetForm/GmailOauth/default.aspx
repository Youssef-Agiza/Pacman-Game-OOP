<%@ Page Language="c#" AutoEventWireup="true" ValidateRequest="false" CodePage="65001" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="EASendMail" %>
<script language="c#" runat="server">
    //  ===============================================================================
    // |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
    // |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
    // |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
    // |    PARTICULAR PURPOSE.                                                    |
    // |    Copyright (c)2015  ADMINSYSTEM SOFTWARE LIMITED                        |
    // |
    // | FILE: default.aspx
    // | SYNOPSIS: Sample ASP.NET for Gmail OAUTH.
    // | Ivan Lui (Ivan@EmailArchitect.NET)
    // |
    //
    /*
     To use Google OAUTH in your application, you must create a project in Google Developers Console.

    - Create your project at https://console.developers.google.com/project.
    - Select your project -> APIs & Services -> Dashboard -> Credentials;
    - Credentials -> Create Credentials -> OAuth client ID -> Web application or Other (Desktop Application). 
      It depends on your application type.

    - Input a name for your application, input your current ASP/ASP.NET URL at Authorized redirect URIs, 
      for example: http://localhost/gmailoauth/default.aspx. (Desktop Application doesn't require this step)
      Click "Create", you will get your client id and client secret.

    - Finally you can also set detail information for your project at Credentials -> OAuth consent screen.

    - If you used https://mail.google.com scope, you should verify your application that is inroduced in cosent screen.
      If you don't verify your application, your application is limited by some conditions.

      You must apply for your client id and client secret, don't use the client id in the sample project, because it is limited now.
      If you got "This app isn't verified" information, please click "advanced" -> Go to ... for test.

    */
    private static string m_client_id = "1072602369179-prgq7tkc834li6ao3fd11r6f2mpc1hdq.apps.googleusercontent.com";
    private static string m_client_secret = "zRqEjYMLJwvfacYA_vUH_6M4";

    private static string m_redirect_uri = "http://localhost/gmailoauth/default.aspx"; // please change it to your current uri
    private static string m_scope = "email%20profile%20https://mail.google.com";
    private static string m_auth_uri = "https://accounts.google.com/o/oauth2/v2/auth";
    private static string m_token_uri = "https://www.googleapis.com/oauth2/v4/token";
    private static string m_plus_api_uri = "https://www.googleapis.com/plus/v1/people/me?fields=emails&access_token=";

   
    private void Page_Load(Object sender, EventArgs e)
    {
        if (txtAccessToken.Value.Length == 0)
        {
            string code = Request.QueryString["code"];
            if (code != null)
            {
                if (code.Length > 0)
                {
                    GetAccessToken(code);
                }
            }
            else
            {
                string authURI = String.Format("{0}?scope={1}&redirect_uri={2}&response_type=code&client_id={3}&prompt=consent&access_type=offline",
                        m_auth_uri, m_scope, m_redirect_uri, m_client_id);
                Response.Redirect(authURI);
                Response.End();
            }
        }
        
        if (lstPort.Items.Count > 0)
            return;

        lstPort.Items.Add("25");
        lstPort.Items.Add("587");
        lstPort.Items.Add("465");

        lstPort.SelectedIndex = 1;
        
        txtSubject.Text = "Gmail Oauth ASP.Net Test";
        string s = "Hi,\r\nThis sample demonstrates how to send email in ASP.NET.\r\n\r\n";
        s += "From:[$from]\r\n";
        s += "To:[$to]\r\n";
        s += "Subject:[$subject]\r\n\r\n";
        s += "This sample project demonstrates how to send email using Gmail OAUTH, \r\n";
        s += "Please create your client_id and client_secret introduced in source code.\r\n\r\n";
        s += "If you got \"This app isn't verified\" information, please click \"advanced\" -> Go to ... for test.\r\n";
        s += "\r\n";

        txtBody.Text = s;
    }

    // Get access_token from Google server.
    private void GetAccessToken(string code)
    {
        System.Text.Encoding encoder = System.Text.Encoding.GetEncoding("utf-8");
     
        try
        {
            string url = m_token_uri;
            HttpWebRequest oClient = WebRequest.Create(url) as HttpWebRequest;
            oClient.Method = "POST";
            oClient.ContentType = "application/x-www-form-urlencoded";

            string client_id = m_client_id;
            string client_secret = m_client_secret;
            string redirect_uri = m_redirect_uri;

            string data = "code=" + code;
            data += "&client_id=" + client_id;
            data += "&client_secret=" + client_secret;
            data += "&redirect_uri=" + redirect_uri;
            data += "&grant_type=authorization_code";
            
            Stream ofs = oClient.GetRequestStream();

            byte[] dt = System.Text.Encoding.UTF8.GetBytes(data);
            ofs.Write(dt, 0, dt.Length);
            ofs.Close();
            
            HttpWebResponse oResponse = oClient.GetResponse() as HttpWebResponse;

            System.IO.StreamReader readStream = new System.IO.StreamReader(oResponse.GetResponseStream(), encoder);
            string responseText = readStream.ReadToEnd();
            
            OAuthResponseParser parser = new OAuthResponseParser();
            parser.Load(responseText);

            txtAccessToken.Value = parser.AccessToken;
            txtRefreshToken.Value = parser.RefreshToken;
            txtTokenExpiresInSeconds.Value = parser.TokenExpiresInSeconds.ToString();
            txtAccessTokenTimeStamp.Value = DateTime.Now.ToString();
            txtUser.Value = parser.EmailInIdToken;
            txtFrom.Text = parser.EmailInIdToken;

            if ((txtAccessToken.Value.Length == 0) || (txtUser.Value.Length == 0))
            {
                string output = "<pre>OAUTH Error:\r\n\r\n";
                output += Server.HtmlEncode(responseText);
                output += "</pre>\r\n";

                Response.Write(output);
                Response.End();
                return;
            }
        }
        catch (WebException ex)
        {
            string errorDesc = ex.Message;
            if ((ex.Response != null))
            {
                System.IO.StreamReader readStream = new System.IO.StreamReader(ex.Response.GetResponseStream(), encoder);
                errorDesc = readStream.ReadToEnd();
            }
            string output = "<pre>OAUTH Error:\r\n\r\n";
            output += Server.HtmlEncode(errorDesc);
            output += "</pre>\r\n";

            Response.Write(output);
            Response.End();
        }
    }
    
    private bool IsAccessTokenExpired()
    {
        DateTime AccessTokenTimeStamp = DateTime.Parse(txtAccessTokenTimeStamp.Value);
        int TokenExpiresInSeconds = Int32.Parse(txtTokenExpiresInSeconds.Value);
        return (AccessTokenTimeStamp.AddSeconds(TokenExpiresInSeconds - 30) < DateTime.Now);
    }

    private void RefreshToken()
    {
        System.Text.Encoding encoder = System.Text.Encoding.GetEncoding("utf-8");
     
        try
        {
            string url = m_token_uri;
            HttpWebRequest oClient = WebRequest.Create(url) as HttpWebRequest;
            oClient.Method = "POST";
            oClient.ContentType = "application/x-www-form-urlencoded";

            string client_id = m_client_id;
            string client_secret = m_client_secret;
            string redirect_uri = m_redirect_uri;

            string data = "client_id=" + client_id;
            data += "&client_secret=" + m_client_secret;
            data += "&refresh_token=" + txtRefreshToken.Value;
            data += "&grant_type=refresh_token";
            
            Stream ofs = oClient.GetRequestStream();

            byte[] dt = System.Text.Encoding.UTF8.GetBytes(data);
            ofs.Write(dt, 0, dt.Length);
            ofs.Close();
            
            HttpWebResponse oResponse = oClient.GetResponse() as HttpWebResponse;

            System.IO.StreamReader readStream = new System.IO.StreamReader(oResponse.GetResponseStream(), encoder);
            string responseText = readStream.ReadToEnd();
            OAuthResponseParser parser = new OAuthResponseParser();
            parser.Load(responseText);

            txtAccessToken.Value = parser.AccessToken;
            txtTokenExpiresInSeconds.Value = parser.TokenExpiresInSeconds.ToString();
            txtAccessTokenTimeStamp.Value = DateTime.Now.ToString();
            
        }
        catch (WebException ex)
        {
            string errorDesc = ex.Message;
            if ((ex.Response != null))
            {
                System.IO.StreamReader readStream = new System.IO.StreamReader(ex.Response.GetResponseStream(), encoder);
                errorDesc = readStream.ReadToEnd();
            }
            string output = "<pre>Refresh Token Error:\r\n\r\n";
            output += Server.HtmlEncode(errorDesc);
            output += "</pre>\r\n";

            Response.Write(output);
            Response.End();
        }
    }

    private void btnSend_Click(object sender, System.EventArgs e)
    {
        if(IsAccessTokenExpired())
        {
            RefreshToken();
        }

        result.Text = "";
        
        if (txtTo.Text.Trim() == "")
        {
            lblDesc.Text = "Please input recipient address!";
            return;
        }

        lblDesc.Text = "";

        //For evaluation usage, please use "TryIt" as the license code, otherwise the 
        //"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
        //"trial version expired" exception will be thrown.

        //For licensed uasage, please use your license code instead of "TryIt", then the object
        //will never expire
        SmtpMail oMail = new SmtpMail("TryIt");

        oMail.Charset = "utf-8";

        
        oMail.From = txtFrom.Text;
        oMail.Subject = txtSubject.Text;

        //To, Cc and Bcc is a AddressCollection object, in C#, it supports implicit converting from string.
        // multiple address are separated with (,;)
        //The syntax is like this: "test@adminsystem.com, test1@adminsystem.com"

        //The example code without implicit converting
        // oMail.To = new AddressCollection("test1@adminsystem.com, test2@adminsystem.com");
        // oMail.To = new AddressCollection("Tester1<test@adminsystem.com>, Tester2<test2@adminsystem.com>");		
        oMail.To = txtTo.Text;
        //You can add more recipient by Add method
        // oMail.To.Add(new MailAddress("tester", "test@adminsystem.com"));


        string fileName = null;
        if (attachment.PostedFile != null)
        {
            fileName = attachment.PostedFile.FileName;
            if (fileName != null && fileName != "")
            {
                try
                {
                    int fileLen = attachment.PostedFile.ContentLength;
                    byte[] content = new byte[fileLen];
                    System.IO.Stream stream = attachment.PostedFile.InputStream;
                    stream.Read(content, 0, fileLen);
                    stream.Close();
                    oMail.AddAttachment(fileName, content);
                }
                catch (Exception exec)
                {
                    lblDesc.Text = String.Format("Exception with add attachment: {0}", exec.ToString());
                    return;
                }
            }
        }

        string body = txtBody.Text;
        body = body.Replace("[$from]", txtFrom.Text);
        body = body.Replace("[$to]", txtTo.Text);
        body = body.Replace("[$subject]", oMail.Subject);

       
        oMail.TextBody = body;

        SmtpClient oSmtp = new SmtpClient();
        //To generate a log file for SMTP transaction, please use
        //oSmtp.LogFileName = "c:\\smtp.log";

        SmtpServer oServer = new SmtpServer(txtServer.Text);
        oServer.Protocol = ServerProtocol.SMTP;
        oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
        oServer.Port = Int32.Parse(lstPort.Text);
        oServer.AuthType = SmtpAuthType.XOAUTH2;

        oServer.User = txtUser.Value;
        oServer.Password = txtAccessToken.Value;
        
        string err = "";
        bool bSmtpConversation = false;
        try
        {
            bSmtpConversation = true;
            oSmtp.SendMail(oServer, oMail);
            result.Text = String.Format("The message was sent to {0} successfully!<br />", oServer.Server);

        }
        catch (SmtpTerminatedException exp)
        {
            err = exp.Message;
        }
        catch (System.Exception exp)
        {
            err = String.Format("Exception: {0}", exp.Message);
        }

        if (err.Length > 0)
        {
            result.Text = Server.HtmlEncode(err);
        }

        if (bSmtpConversation)
        {
            result.Text += "<pre>SMTP Log\r\n\r\n";
            result.Text += Server.HtmlEncode(oSmtp.SmtpConversation);
            result.Text += "</pre>\r\n";
        }

    }

</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Gmail OAUTH, ASP.NET, C# Sample For EASendMail</title>
    <meta http-equiv="Content-Type" content="text-html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="sample.css" />
</head>
<body>
    <div id="s_title">
       Gmail OAUTH, ASP.NET, C# Sample for EASendMail SMTP Component</div>
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
                    <input type="File" id="attachment" name="attachment" runat="server" width="95%" />
                </td>
            </tr>
            <tr>
                <td>
                    Email Body
                </td>
                <td>
                    <asp:TextBox ID="txtBody" runat="server" TextMode="multiline" Width="95%" Height="120px"></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnSend" runat="server" Width="131px" Text="Send" OnClick="btnSend_Click">
                    </asp:Button> - <a href="default.aspx">Reset</a>
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
