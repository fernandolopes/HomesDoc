using HomesDoc.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HomesDoc.UnitTest
{
    [TestClass]
    public class DocumentoClientTest
    {
        public DocumentoClient Client { get; set; }
        public NaturezaCliente NClient { get; set; }
        public PropriedadeCliente PClient { get; set; }

        [TestInitialize]
        public void Iniciacao()
        {
            Client = new DocumentoClient(Dados.UrlBase, Dados.ClientId, Dados.AccessToken);
            NClient = new NaturezaCliente(Dados.UrlBase, Dados.ClientId, Dados.AccessToken);
            PClient = new PropriedadeCliente(Dados.UrlBase, Dados.ClientId, Dados.AccessToken);
        }

        //[TestMethod]
        public void RemoverDocumento()
        {
            Task.Run(() => Client.Remover(0)).Wait();
        }

        //[TestMethod]
        public void TestMethod2()
        {
            string fileName = @"C:\Users\Fernando\Desktop\boletoLicenciamento.pdf";

            byte[] bytes = File.ReadAllBytes(fileName);
            var naturezas = Task.Run(() => NClient.Listar()).Result?.ToList();
            var natureza = naturezas?.Find(x => x.Name.Equals("Recursos Humanos2"));
            var propriedades = Task.Run(() => PClient.Listar()).Result?.ToList();
            var propriedadeE = propriedades?.Find(x => x.Name.Equals("Email Usuario"));
            var propriedadeT = propriedades?.Find(x => x.Name.Equals("Tipo Documento"));

            var dic = new Dictionary<string, string>();
            dic.Add($"{propriedadeE.Id}", "fernandolopes.s@gmail.com");
            dic.Add($"{propriedadeT.Id}", "RG");
            //bool adicionado = Task.Run(() => PClient.AddValor(propriedadeT.Id, "RG")).Result;

            bool resp = Task.Run(() => Client.Upload(bytes, "indice.jpg", natureza.Id, dic)).Result;
            Assert.IsTrue(resp);
        }
    }
}
