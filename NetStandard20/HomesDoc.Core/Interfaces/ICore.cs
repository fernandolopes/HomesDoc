using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomesDoc.Core.Interfaces
{
    public interface ICore<T>
    {
        Task<IEnumerable<T>> Listar();
        Task<T> Criar(T item);
        Task<bool> GerarPermissao(T item);
        Task Remover(T item);
    }
}
