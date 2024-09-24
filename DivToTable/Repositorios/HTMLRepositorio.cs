using DivToTable.Data;
using DivToTable.Model;
using DivToTable.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace DivToTable.Repositorios
{
    public class HTMLRepositorio : IHTMLRepositorio
    {
        private readonly HTMLContext _dbcontext;
        public HTMLRepositorio(HTMLContext htmlContext)
        {
            _dbcontext = htmlContext;
        }

        public async Task<List<HTMLModel>> ListarHTML()
        {
            return await _dbcontext.DivtoTable.ToListAsync();
        }
        public async Task<HTMLModel> BuscarHTML(int id)
        {
            return await _dbcontext.DivtoTable.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<HTMLModel> EditarHTML(HTMLModel html, int id)
        {
           HTMLModel htmlID = await BuscarHTML(id);
            if (htmlID == null)
            {
                throw new Exception($"O conteúdo HTML não foi encontrado, id = {id}.");
            }
            htmlID.HTML = html.HTML; 

            _dbcontext.DivtoTable.Update(htmlID);
            await _dbcontext.SaveChangesAsync();

            return htmlID;
        }

        public async Task<HTMLModel> AdicionarHTML(HTMLModel html)
        {
            await _dbcontext.DivtoTable.AddAsync(html);
            await _dbcontext.SaveChangesAsync();

            return html;
        }
        
        public async Task<bool> Apagar(int id)
        {
            HTMLModel htmlID = await BuscarHTML(id);
            if (htmlID == null)
            {
                throw new Exception($"O conteúdo HTML não foi encontrado, id = {id}.");
            }

            _dbcontext.DivtoTable.Remove(htmlID);
            await  _dbcontext.SaveChangesAsync();
            return true;
        }

        
    }
}
