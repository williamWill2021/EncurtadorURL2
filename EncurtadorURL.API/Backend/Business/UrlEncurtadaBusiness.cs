using EncurtadorURL.API.Backend.Interfaces.IBusiness;
using EncurtadorURL.API.Backend.Interfaces.IDataAccess;
using EncurtadorURL.API.Backend.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace EncurtadorURL.API.Backend.Business
{
    public class UrlEncurtadaBusiness : IUrlEncurtadaBusiness
    {
        //private const string API_URL = "https://localhost:7234/api/";

        private readonly IUrlEncurtadaDataAccess DataAccess;
        public UrlEncurtadaBusiness(IUrlEncurtadaDataAccess _da)
        {
            this.DataAccess = _da;
        }

        public Task<UrlEncurtadas> Create(UrlEncurtadas instance)
        {
            if (instance == null)
                throw new ApplicationException("Dados da instance url não informado.");
            if (string.IsNullOrEmpty(instance.Url))
                throw new ApplicationException("Url não informada.");

            return this.DataAccess.Create(instance);
        }
        public Task<UrlEncurtadas> Update(UrlEncurtadas instance)
        {
            if (instance == null)
                throw new ApplicationException("Dados da instance url não informado.");

            return this.DataAccess.Update(instance);
        }
        public UrlEncurtadas EncurtarUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ApplicationException("URL para encurtar não informada.");

            UrlEncurtadas novaUrl = new UrlEncurtadas(url);

            return novaUrl;
        }
        public bool VerificarUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ApplicationException("URL para encurtar não informada.");

            return this.DataAccess.VerificarUrl(url);
        }
        public UrlEncurtadas? ReadBy_ID(long? id)
        {
            if (id == null)
                throw new ApplicationException("ID para busca não informado.");

            return this.DataAccess.ReadBy_ID(id);
        }
        public UrlEncurtadas? ReadBy_Url(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ApplicationException("URL para busca não informada.");

            return this.DataAccess.ReadBy_Url(url);
        }
        public UrlEncurtadas? ReadBy_ShortUrl(string shortUrl)
        {
            if (string.IsNullOrEmpty(shortUrl))
                throw new ApplicationException("Short URL para busca não informada.");

            return this.DataAccess.ReadBy_ShortUrl(shortUrl);
        }
        public Task<UrlEncurtadas?> ReadBy_IdShortUrl(string idShortUrl)
        {
            if (string.IsNullOrEmpty(idShortUrl))
                throw new ApplicationException("IdShort URL para busca não informada.");

            return this.DataAccess.ReadBy_IdShortUrl(idShortUrl);
        }
        public List<UrlEncurtadas> ObterTop5FromUrlsJson()
        {
            string content = System.IO.File.ReadAllText($"{Directory.GetCurrentDirectory()}/urls.json");
            var listaUrls = JsonConvert.DeserializeObject<IEnumerable<UrlEncurtadas>>(content);
            int quantidadeRegistros = 5;

            if (listaUrls == null)
                throw new ApplicationException("Não foi possível efetuar a leitura do arquivo urls.json.");

            List<UrlEncurtadas> result = new List<UrlEncurtadas>();
            listaUrls.OrderByDescending(x => x.Hits).ToList().ForEach(x =>
            {
                if (result.Count >= quantidadeRegistros)
                    return;

                result.Add(x);
            });

            return result;
        }




        //private string ObterShortUrl(string url)
        //{
        //    if (string.IsNullOrEmpty(url))
        //        throw new ApplicationException("URL não informada.");

        //    string shortUrl = $"{API_URL}{this.ObterAlfanumericoAleatorio()}";
        //    return shortUrl;
        //}
        //private string ObterAlfanumericoAleatorio()
        //{
        //    int tamanho = 5;
        //    string caracteres = "abcdefghijklmnopqrstuvwxyz0123456789";
        //    var random = new Random();
        //    var result = new string(Enumerable.Repeat(caracteres, tamanho).Select(s => s[random.Next(s.Length)]).ToArray());
        //    return result;
        //}
    }
}
