using EncurtadorURL.BackEnd.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncurtadorURL.BackEnd.Business
{
    public class EncurtarURLBusiness
    {
        private string ApiURL
            => Configuration.ApiURL;

        public string ObterShortUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ApplicationException("URL não informada.");

            string shortUrl = $"{this.ApiURL}{ObterAlfanumericoAleatorio()}";

            return shortUrl;
        }

        public static string ObterAlfanumericoAleatorio()
        {
            int tamanho = 5;
            string caracteres = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(caracteres, tamanho).Select(s => s[random.Next(s.Length)]).ToArray());
            return result;
        }
    }
}
