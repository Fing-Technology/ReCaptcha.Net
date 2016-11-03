using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using ReCaptcha.Net.Helpers;

namespace ReCaptcha.Net
{
    public class ReCaptchaValidator
    {
        private const string VerifyUrl = "https://www.google.com/recaptcha/api/siteverify";

        public string SecretKey { get; set; }

        public string Response { get; set; }

        private string _remoteIp;
        public string RemoteIp
        {
            get { return _remoteIp; }
            set
            {
                IPAddress ip = IPAddress.Parse(value);

                if (ip.AddressFamily != AddressFamily.InterNetwork &&
                    ip.AddressFamily != AddressFamily.InterNetworkV6)
                {
                    throw new ArgumentException("Expecting an IP address, got " + ip);
                }

                _remoteIp = ip.ToString();
            }
        }

        private void CheckNotNull(object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public ReCaptchaResponse Validate()
        {
            CheckNotNull(SecretKey, "SecretKey");
            CheckNotNull(Response, "Response");
            CheckNotNull(RemoteIp, "RemoteIp");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(VerifyUrl);
            // to avoid issues with Expect headers
            request.ProtocolVersion = HttpVersion.Version10;
            request.Timeout = 30 * 1000 /* 30 seconds */;
            request.Method = "POST";
            request.UserAgent = "reCAPTCHA/ASP.NET";
            request.ContentType = "application/x-www-form-urlencoded";

            string formdata = string.Format("secret={0}&response={1}&remoteip={2}",
                                    HttpUtility.UrlEncode(SecretKey),
                                    HttpUtility.UrlEncode(Response),
                                    HttpUtility.UrlEncode(RemoteIp));

            byte[] formbytes = Encoding.ASCII.GetBytes(formdata);

            using (Stream requestStream = request.GetRequestStream())
                requestStream.Write(formbytes, 0, formbytes.Length);

            string responseValue = string.Empty;
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    //_responseHeaders = response.Headers;
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                                reader.Close();
                            }
                            responseStream.Close();
                        }
                    }
                    response.Close();
                }
            }
            catch (WebException ex)
            {
                var response = (HttpWebResponse)ex.Response;
                //_responseHeaders = response.Headers;
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                            reader.Close();
                        }
                        responseStream.Close();
                    }
                }
                response.Close();
                throw new Exception(responseValue);
            }
            return JsonHelper.Deserialize<ReCaptchaResponse>(responseValue);
        }
    }
}
