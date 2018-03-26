using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomesDoc.Core;
using System.Threading.Tasks;

namespace HomesDoc.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        public PropriedadeCliente Client { get; set; }

        [TestInitialize]
        public void Iniciar()
        {
            Client = new PropriedadeCliente(Dados.UrlBase, Dados.ClientId, Dados.AccessToken);
        }

        [TestMethod]
        public void TestMethod1()
        {
            bool adicionado = Task.Run(() => Client.AddValor(0, "")).Result;
        }
    }
}
