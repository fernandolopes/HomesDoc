using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace HomesDoc.Core
{
    public abstract class Core
    {
        protected Uri UriBase { get; set; }
        protected string ClientId { get; set; }
        protected string AccessToken { get; set; }
        protected HttpClientHandler HttpClientHandler { get; set; }

        public Core(Uri uriBase, string clientId, string accessToken, HttpClientHandler httpClientHandler = null)
        {
            UriBase = uriBase;
            ClientId = clientId;
            AccessToken = accessToken;
            IgnoreBadCertificates();
            HttpClientHandler = httpClientHandler;

            if (httpClientHandler == null)
            {
                HttpClientHandler = new HttpClientHandler();
            }
        }

        protected void IgnoreBadCertificates()
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);
        }

        private bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
