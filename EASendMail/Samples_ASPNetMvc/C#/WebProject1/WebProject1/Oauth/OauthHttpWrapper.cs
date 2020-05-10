using System;
using System.IO;
using System.Net;
using System.Text;

namespace EASendMail.Oauth
{
    public class OauthHttpWrapper
    {
        public OauthHttpWrapper(OauthProvider provider)
        {
            _provider = provider;
        }

        OauthProvider _provider = null;
        public OauthProvider Provider
        {
            get { return _provider; }
        }

        public bool IsAccessTokenExpired
        {
            get
            {
                return (_provider.AccessTokenTimeStamp.AddSeconds(
                    _provider.TokenExpiresInSeconds - 30) < DateTime.Now);
            }
        }

        string _authorizationCode = string.Empty;
        public string AuthorizationCode
        {
            get { return _authorizationCode; }
            set { _authorizationCode = value; }
        }

        public void RequestAccessTokenAndUserEmail()
        {
            if (string.IsNullOrWhiteSpace(AuthorizationCode))
            {
                throw new Exception("Authorization code is not existed!");
            }

            string requestData = _provider.TokenRequestData(AuthorizationCode);
            string responseText = _postString(_provider.TokenUri, requestData);

            OAuthResponseParser parser = new OAuthResponseParser();
            parser.Load(responseText);

            _provider.AccessToken = parser.AccessToken;
            _provider.RefreshToken = parser.RefreshToken;
            _provider.UserEmail = parser.EmailInIdToken;
            _provider.TokenExpiresInSeconds = parser.TokenExpiresInSeconds;

            if (string.IsNullOrWhiteSpace(_provider.AccessToken))
            {
                throw new Exception("Failed to request access token!");
            }

            if (string.IsNullOrWhiteSpace(_provider.UserEmail))
            {
                throw new Exception("Failed to request user email address!");
            }
        }

        public void RefreshAccessToken()
        {
            if (string.IsNullOrWhiteSpace(_provider.RefreshToken))
            {
                throw new Exception("Refresh token is not existed!");
            }

            string requestData = _provider.RefreshTokenRequestData();
            string responseText = _postString(_provider.TokenUri, requestData);

            OAuthResponseParser parser = new OAuthResponseParser();
            parser.Load(responseText);

            _provider.AccessToken = parser.AccessToken;
            if (!string.IsNullOrEmpty(parser.RefreshToken))
            {
                _provider.RefreshToken = parser.RefreshToken;
            }

            if (parser.TokenExpiresInSeconds > 0)
            {
                _provider.TokenExpiresInSeconds = parser.TokenExpiresInSeconds;
            }
        }

        string _postString(string uri, string requestData)
        {
            HttpWebRequest httpRequest = WebRequest.Create(uri) as HttpWebRequest;
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/x-www-form-urlencoded";

            using (Stream requestStream = httpRequest.GetRequestStream())
            {
                byte[] requestBuffer = Encoding.UTF8.GetBytes(requestData);
                requestStream.Write(requestBuffer, 0, requestBuffer.Length);
                requestStream.Close();
            }

            try
            {
                HttpWebResponse httpResponse = httpRequest.GetResponse() as HttpWebResponse;
                using (StreamReader responseStream = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
                {
                    return responseStream.ReadToEnd();
                }
            }
            catch (WebException ep)
            {
                if (ep.Status == WebExceptionStatus.ProtocolError)
                {
                    using (StreamReader responseStream = new StreamReader(ep.Response.GetResponseStream(), Encoding.UTF8))
                    {
                        string errorDescription = responseStream.ReadToEnd();
                    }
                }
                throw ep;
            }

        }
    }
}
