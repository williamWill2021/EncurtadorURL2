using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EncurtadorURL.API.Backend.Models;
using EncurtadorURL.API.Backend.DataAccess.Context;
using EncurtadorURL.API.Backend.Interfaces.IBusiness;
using EncurtadorURL.API.Backend.Interfaces.IDataAccess;
using EncurtadorURL.API.Backend.Business;

namespace EncurtadorURL.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlEncurtadasController : ControllerBase
    {
        private readonly IUrlEncurtadaBusiness UrlEncurtadaBusiness;
        private List<string> UrlsGeradas = null;

        public UrlEncurtadasController(IUrlEncurtadaBusiness _business)
        {
            UrlEncurtadaBusiness = _business;
            this.UrlsGeradas = new List<string>();
        }

        // GET: UrlEncurtadas/ObterUrl/a1df3
        [HttpGet]
        public async Task<ActionResult<string>> ObterUrl(string idShortUrl)
        {
            if (string.IsNullOrEmpty(idShortUrl))
                return NotFound();

            var urlEncurtadas = await this.UrlEncurtadaBusiness.ReadBy_IdShortUrl(idShortUrl);
            if (urlEncurtadas == null)
                return NotFound();

            urlEncurtadas.Hits += 1;
            await this.UrlEncurtadaBusiness.Update(urlEncurtadas);

            return Redirect(urlEncurtadas.Url);
        }

        // POST: UrlEncurtadas/EncurtarUrl
        [HttpPost("EncurtarUrl")]
        public async Task<ActionResult<string>> EncurtarUrl([Bind("url")] string url)
        {
            if (string.IsNullOrEmpty(url))
                return BadRequest("Url não informada.");

            string result = string.Empty;

            int limiteTentativas = 10;
            int tentativas = default(int);
            UrlEncurtadas? urlResult = null;

            // Verificando se URL já foi cadastrada, caso sim, retorna o registro encontrado
            urlResult = this.UrlEncurtadaBusiness.ReadBy_Url(url);
            if (urlResult != null)
                return urlResult.ShortUrl;

            // Verificando se URL existe
            bool urlExiste = this.UrlEncurtadaBusiness.VerificarUrl(url);
            if (!urlExiste)
                return BadRequest("URL não existe");

            do
            {
                // Quando URl não cadastrada, vamos encurtar por 10 tentativas, caso necessário, e cadastrar em caso de sucesso
                urlResult = this.UrlEncurtadaBusiness.EncurtarUrl(url);
                bool jaExisteShortUrl = this.UrlEncurtadaBusiness.ReadBy_ShortUrl(urlResult.ShortUrl) != null;
                tentativas++;

                if (!jaExisteShortUrl)
                    break;
                else if (tentativas == limiteTentativas && jaExisteShortUrl)
                {
                    urlResult = null;
                    break;
                }

            } while (tentativas <= limiteTentativas);

            if (urlResult != null)
            {
                await this.UrlEncurtadaBusiness.Create(urlResult);
                result = urlResult.ShortUrl;
                this.UrlsGeradas.Add(result);
            }

            return result;
        }

    }
}
