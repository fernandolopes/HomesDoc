using HomesDoc.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HomesDoc.UnitTest
{
    [TestClass]
    public class NaturezaClientTest
    {
        public NaturezaCliente Client { get; set; }

        [TestInitialize]
        public void Iniciacao()
        {
            Client = new NaturezaCliente(Dados.UrlBase, Dados.ClientId, Dados.AccessToken);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var lista = Task.Run(() => Client.Listar()).Result;
            Assert.IsTrue(condition: lista.Count() == 30);
        }
    }
}
