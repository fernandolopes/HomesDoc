using HomesDoc.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HomesDoc.Core
{
    public class DocumentoClient : Core, IDocumentoCore
    {
        public DocumentoClient(Uri uriBase, string clientId, string accessToken, HttpClientHandler httpClientHandler = null) : base(uriBase, clientId, accessToken, httpClientHandler)
        { }

        public async Task Remover(int Id)
        {
            try
            {
                using (var client = new HttpClient(HttpClientHandler))
                {
                    client.DefaultRequestHeaders.Add("clientId", ClientId);
                    client.DefaultRequestHeaders.Add("accessToken", AccessToken);
                    client.BaseAddress = UriBase;

                    HttpRequestMessage request = new HttpRequestMessage(
                                                        HttpMethod.Delete, 
                                                        $"holmes/api/document/{Id}"
                                                     );
                    request.Headers.Clear();
                    request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");

                    var resp = await client.SendAsync(request);
                    if (resp.IsSuccessStatusCode)
                    {
                        var conteudo = await resp.Content?.ReadAsStringAsync();
                    }
                    else
                    {
                        var conteudo = await resp.Content?.ReadAsStringAsync();
                        Console.WriteLine(conteudo);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<bool> Upload(byte[] imageBytes, string fileName, int naturezaId, Dictionary<string, string> dic, int thumbPage = 0)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("clientId", ClientId);
                    client.DefaultRequestHeaders.Add("accessToken", AccessToken);
                    client.BaseAddress = UriBase;

                    var content = new MultipartFormDataContent();
                    var imageContent = new ByteArrayContent(imageBytes);
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse($"{Funcoes.GetImageType(imageBytes)}");

                    content.Add(imageContent, "file", fileName);
                    content.Add(new StringContent($"{thumbPage}"), "thumbPage");
                    content.Add(new StringContent($"{naturezaId}"), "natureId");
                    foreach (var i in dic)
                    {
                        content.Add(new StringContent(i.Value), i.Key);
                    }

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "holmes/api/upload")
                    {
                        Content = content
                    };

                    var resp = await client.SendAsync(request);

                    if (resp.IsSuccessStatusCode)
                    {
                        var conteudo = await resp.Content?.ReadAsStringAsync();
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
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
