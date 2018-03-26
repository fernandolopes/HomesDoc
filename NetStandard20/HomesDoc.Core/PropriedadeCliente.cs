using DocumentosCurriculoService.Entity;
using HomesDoc.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace HomesDoc.Core
{
    public class PropriedadeCliente : Core, ICore<Propriedade>
    {
        public PropriedadeCliente(Uri uriBase, string clientId, string accessToken, HttpClientHandler httpClientHandler = null) : base(uriBase, clientId, accessToken, httpClientHandler)
        { }

        public async Task<bool> AddValor(int propriedadeId, string valor)
        {
            try
            {
                var httpMessageHandler = new WinHttpHandler();

                using (var client = new HttpClient(httpMessageHandler))
                {
                    client.DefaultRequestHeaders.Add("clientId", ClientId);
                    client.DefaultRequestHeaders.Add("accessToken", AccessToken);
                    client.BaseAddress = UriBase;

                    HttpRequestMessage request = new HttpRequestMessage(
                        HttpMethod.Put,
                        $"holmes/api/properties/{propriedadeId}/value?value={valor}"
                    );

                    var resp = await client.SendAsync(request);
                    if (resp.IsSuccessStatusCode)
                    {
                        await resp.Content?.ReadAsStringAsync();
                        return true;
                    }
                    else
                    {
                        var conteudo = await resp.Content?.ReadAsStringAsync();
                        Console.WriteLine(conteudo);
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

        public async Task<IEnumerable<Propriedade>> Listar()
        {
            using (var client = new HttpClient(HttpClientHandler))
            {
                client.DefaultRequestHeaders.Add("clientId", ClientId);
                client.DefaultRequestHeaders.Add("accessToken", AccessToken);
                client.BaseAddress = UriBase;
                var resp = await client.GetAsync("holmes/api/properties");

                if (resp.IsSuccessStatusCode)
                {
                    var conteudo = await resp.Content?.ReadAsStringAsync();
                    List<Propriedade> lista = JsonConvert.DeserializeObject<List<Propriedade>>(conteudo);
                    return lista;
                }
                throw new ArgumentException("Erro ao tentar listar as propriedades.");
            }
        }

        public async Task<Propriedade> Criar(Propriedade item)
        {
            using (var client = new HttpClient(HttpClientHandler))
            {
                client.DefaultRequestHeaders.Add("clientId", ClientId);
                client.DefaultRequestHeaders.Add("accessToken", AccessToken);
                client.BaseAddress = UriBase;
                MultipartFormDataContent form = new MultipartFormDataContent();

                form.Add(new StringContent(item.Name), "name");
                form.Add(new StringContent(item.PropertyType), "propertyType");
                form.Add(new StringContent($"{item.RelatedList}"), "relatedList");
                var resp = await client.PostAsync("holmes/api/properties", form);
                if (resp.IsSuccessStatusCode)
                {
                    var conteudo = await resp.Content?.ReadAsStringAsync();
                    Propriedade propriedade = JsonConvert.DeserializeObject<Propriedade>(conteudo);
                    return propriedade;
                }
                throw new ArgumentException("Erro ao tentar criar uma nova natureza.");
            }
        }

        public async Task<bool> GerarPermissao(Propriedade item)
        {
            using (var client = new HttpClient(HttpClientHandler))
            {
                client.DefaultRequestHeaders.Add("clientId", ClientId);
                client.DefaultRequestHeaders.Add("accessToken", AccessToken);
                client.BaseAddress = UriBase;
                MultipartFormDataContent form = new MultipartFormDataContent();

                form.Add(new StringContent($"{item.Id}"), "propertyId");

                var resp = await client.PostAsync($"holmes/api/nature/{item.NaturezaId}/property", form);
                if (resp.IsSuccessStatusCode)
                {
                    var conteudo = await resp.Content?.ReadAsStringAsync();
                }
                throw new ArgumentException("Erro ao tentar dar Permissão a propriedade.");
            }
        }

        public async Task Remover(Propriedade item)
        {
            try
            {
                using (var client = new HttpClient(HttpClientHandler))
                {
                    client.DefaultRequestHeaders.Add("clientId", ClientId);
                    client.DefaultRequestHeaders.Add("accessToken", AccessToken);
                    client.BaseAddress = UriBase;

                    await client.DeleteAsync($"holmes/api/properties/{item.Id}");
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
