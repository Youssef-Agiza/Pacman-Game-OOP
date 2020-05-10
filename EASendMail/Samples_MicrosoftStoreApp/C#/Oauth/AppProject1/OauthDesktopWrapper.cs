using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EASendMail;

namespace EASendMail.Oauth
{
    public class OauthDesktopWrapper
    {
        public OauthDesktopWrapper(OauthProvider provider)
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

        public async Task RequestAccessTokenAndUserEmailAsync()
        {
            if (string.IsNullOrWhiteSpace(AuthorizationCode))
            {
                throw new Exception("Authorization code is not existed!");
            }

            string requestData = _provider.TokenRequestData(AuthorizationCode);
            string responseText = await _postStringAsync(_provider.TokenUri, requestData);

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

        public async Task RefreshAccessTokenAsync()
        {
            if (string.IsNullOrWhiteSpace(_provider.RefreshToken))
            {
                throw new Exception("Refresh token is not existed!");
            }

            string requestData = _provider.RefreshTokenRequestData();
            string responseText = await _postStringAsync(_provider.TokenUri, requestData);

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

        private async Task<string> _postStringAsync(string uri, string requestData)
        {
            HttpWebRequest httpRequest = WebRequest.Create(uri) as HttpWebRequest;
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/x-www-form-urlencoded";

            using (var stream = await httpRequest.GetRequestStreamAsync())
            { 
                var buffer = Encoding.UTF8.GetBytes(requestData);
                await stream.WriteAsync(buffer, 0, buffer.Length);
            }

            WebResponse httpResponse = await httpRequest.GetResponseAsync();
            using (var stream = httpResponse.GetResponseStream())
            { 
                var buffer = await ReadStreamAsync(stream, 0);
                return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            }
        }

        private async Task<byte[]> ReadStreamAsync(Stream stream, int InitSize)
        {
            int readsize = 8192;
            if (InitSize <= 0)
            {
                InitSize = readsize * 2;
            }
            int bufsize = InitSize + readsize;
            byte[] buf = new byte[bufsize + 1];
            int read = 0;
            int cursize = 0;
            while (true)
            {

                read = await stream.ReadAsync(buf, cursize, readsize);
                if (read <= 0)
                    break;

                cursize += read;

                if (bufsize <= readsize + cursize)
                {
                    if (bufsize < 1024 * 1024)
                    {
                        bufsize = bufsize * 2 + readsize;
                    }
                    else
                    {
                        bufsize = bufsize + 1024 * 1024 + readsize;
                    }

                    byte[] t = new byte[bufsize];
                    System.Buffer.BlockCopy(buf, 0, t, 0, cursize);
                    buf = t;
                }
            }

            byte[] data = new byte[cursize];
            Buffer.BlockCopy(buf, 0, data, 0, cursize);

            return data;
        }

    }
}
