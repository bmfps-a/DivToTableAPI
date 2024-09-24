using DivToTable.Model;

namespace DivToTable.Repositorios.Interfaces
{
    public interface IHTMLRepositorio
    {
        Task<List<HTMLModel>> ListarHTML();
        Task<HTMLModel> BuscarHTML(int id);
        Task<HTMLModel> EditarHTML(HTMLModel html,int id);
        Task<HTMLModel> AdicionarHTML(HTMLModel html);
        Task<bool> Apagar(int id);
        
    }
}
