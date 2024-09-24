using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using DivToTable.Repositorios.Interfaces;
using DivToTable.Model;
using System.Web;

namespace DivToTable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HTMLController : ControllerBase
    {
        private readonly IHTMLRepositorio _htmlrepositorio;

        public HTMLController(IHTMLRepositorio htmlrepositorio)
        {
            _htmlrepositorio = htmlrepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<HTMLModel>>> ListarHTML()
        {
            List<HTMLModel> result = await _htmlrepositorio.ListarHTML();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HTMLModel>> BuscarHTML(int id)
        {
            HTMLModel resultID = await _htmlrepositorio.BuscarHTML(id);
            return Ok(resultID);
        }

        [HttpPost]
        public async Task<ActionResult<HTMLModel>> AdicionarHTML([FromBody] HTMLModel htmlModel)
        {
            if (string.IsNullOrWhiteSpace(htmlModel.HTML))
            {
                return BadRequest("O campo HTML não pode estar vazio.");
            }

            
            string decodedHtml = HttpUtility.HtmlDecode(htmlModel.HTML);

            var doc = new HtmlDocument();
            doc.LoadHtml(decodedHtml);

            
            var divNodes = doc.DocumentNode.SelectNodes("//div");
            if (divNodes != null)
            {
                foreach (var div in divNodes)
                {
                    div.Name = "table";
                }
            }

            htmlModel.HTML = doc.DocumentNode.OuterHtml;

            string encodedHtml = HttpUtility.HtmlEncode(htmlModel.HTML);

            htmlModel.HTML = encodedHtml;

            await _htmlrepositorio.AdicionarHTML(htmlModel);

            return Ok("HTML transformado e salvo com sucesso.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HTMLModel>> Atualizar([FromBody] HTMLModel html, int id)
        {
            html.Id = id;

            
            string decodedHtml = HttpUtility.HtmlDecode(html.HTML);

            var doc = new HtmlDocument();
            doc.LoadHtml(decodedHtml);

            var divNodes = doc.DocumentNode.SelectNodes("//div");
            if (divNodes != null)
            {
                foreach (var div in divNodes)
                {
                    div.Name = "table";
                }
            }

            html.HTML = doc.DocumentNode.OuterHtml;

            HTMLModel code = await _htmlrepositorio.EditarHTML(html, id);
            return Ok(code);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarHTML(int id)
        {
            HTMLModel htmlExistente = await _htmlrepositorio.BuscarHTML(id);
            if (htmlExistente == null)
            {
                return NotFound($"HTML com ID {id} não foi encontrado.");
            }

            await _htmlrepositorio.Apagar(id);
            return Ok($"HTML com ID {id} foi removido com sucesso.");
        }
    }
}
