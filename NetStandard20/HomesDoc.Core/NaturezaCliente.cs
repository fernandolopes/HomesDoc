using DocumentosCurriculoService.Entity;
using HomesDoc.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomesDoc.Core
{
    public class NaturezaCliente : Core, ICore<Natureza>
    {
        public NaturezaCliente(Uri uriBase, string clientId, string accessToken, HttpClientHandler httpClientHandler = null) : base(uriBase, clientId, accessToken, httpClientHandler)
        { }

        public async Task<IEnumerable<Natureza>> Listar()
        {
            using (var client = new HttpClient(HttpClientHandler))
            {
                client.DefaultRequestHeaders.Add("clientId", ClientId);
                client.DefaultRequestHeaders.Add("accessToken", AccessToken);
                client.BaseAddress = UriBase;
                var resp = await client.GetAsync("holmes/api/nature");

                if (resp.IsSuccessStatusCode)
                {
                    var conteudo = await resp.Content?.ReadAsStringAsync();
                    List<Natureza> lista = JsonConvert.DeserializeObject<List<Natureza>>(conteudo);
                    return lista;
                }

                throw new ArgumentException("Erro ao tentar acessar as naturezas.");
            }
        }

        public async Task<Natureza> Criar(Natureza item)
        {
            using (var client = new HttpClient(HttpClientHandler))
            {
                client.DefaultRequestHeaders.Add("clientId", ClientId);
                client.DefaultRequestHeaders.Add("accessToken", AccessToken);
                client.BaseAddress = UriBase;
                MultipartFormDataContent form = new MultipartFormDataContent();
                form.Add(new StringContent(item.Name), "name");
                form.Add(new StringContent($"{item.AllowTags}"), "allowTags");
                form.Add(new StringContent($"{item.ValidationRequired}"), "validationRequired");

                var resp = await client.PostAsync("holmes/api/nature", form);
                if (resp.IsSuccessStatusCode)
                {
                    var conteudo = await resp.Content?.ReadAsStringAsync();
                    Natureza natureza = JsonConvert.DeserializeObject<Natureza>(conteudo);
                    return natureza;
                }
                throw new ArgumentException("Erro ao tentar criar uma nova natureza.");
            }
        }

        public async Task<bool> GerarPermissao(Natureza item)
        {
            try
            {
                using (var client = new HttpClient(HttpClientHandler))
                {
                    client.DefaultRequestHeaders.Add("clientId", ClientId);
                    client.DefaultRequestHeaders.Add("accessToken", AccessToken);

                    client.BaseAddress = UriBase;
                    var list = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("profileId", $"{item.GrupoId}"),
                        new KeyValuePair<string, string>("natureId", $"{item.Id}"),
                        new KeyValuePair<string, string>("type", item.Permission)
                    };
                    FormUrlEncodedContent form = new FormUrlEncodedContent(list);

                    form.Headers.Clear();
                    form.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, "holmes/api/profile/permission");
                    request.Content = form;
                    var resp = await client.SendAsync(request);
                    if (resp.IsSuccessStatusCode)
                    {
                        await resp.Content?.ReadAsStringAsync();
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task Remover(Natureza item)
        {
            try
            {
                using (var client = new HttpClient(HttpClientHandler))
                {
                    client.DefaultRequestHeaders.Add("clientId", ClientId);
                    client.DefaultRequestHeaders.Add("accessToken", AccessToken);
                    client.BaseAddress = UriBase;

                    await client.DeleteAsync($"holmes/api/nature/{item.Id}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
