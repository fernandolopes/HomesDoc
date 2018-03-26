using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomesDoc.Core.Interfaces
{
    public interface IDocumentoCore
    {
        Task<bool> Upload(byte[] imageBytes, string fileName, int naturezaId, Dictionary<string, string> dic, int thumbPage = 0);
        Task Remover(int Id);
    }
}
