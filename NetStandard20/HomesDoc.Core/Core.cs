using HomesDoc.Core.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Grupo>> GetGrupos()
        {
            using (var client = new HttpClient(HttpClientHandler))
            {
                client.DefaultRequestHeaders.Add("clientId", ClientId);
                client.DefaultRequestHeaders.Add("accessToken", AccessToken);
                client.BaseAddress = UriBase;
                var resp = await client.GetAsync("holmes/api/profile");
                if (resp.IsSuccessStatusCode)
                {
                    var conteudo = await resp.Content?.ReadAsStringAsync();
                    List<Grupo> lista = JsonConvert.DeserializeObject<List<Grupo>>(conteudo);
                    return lista;
                }
                throw new ArgumentException("Erro ao tentar buscar um grupo pelo perfil.");
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
