namespace EncurtadorURL.API.Backend.Models
{
    public class UrlEncurtadas
    {
        private const string API_URL = "https://localhost:7234/api/";

        public UrlEncurtadas() { }
        public UrlEncurtadas(string urlEncurtar)
        {
            if (!string.IsNullOrEmpty(urlEncurtar))
            {
                this.Url = urlEncurtar;
                this.IdShortUrl = this.ObterAlfanumericoAleatorio();
                this.ShortUrl = $"{API_URL}{this.IdShortUrl}";
            }
        }

        public long? Id { get; set; }
        public int Hits { get; set; }
        public string Url { get; set; }
        public string ShortUrl { get; set; }
        public string IdShortUrl { get; set; }


        private string ObterShortUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ApplicationException("URL não informada.");

            string shortUrl = $"{API_URL}{this.ObterAlfanumericoAleatorio()}";
            return shortUrl;
        }
        private string ObterAlfanumericoAleatorio()
        {
            int tamanho = 5;
            string caracteres = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(caracteres, tamanho).Select(s => s[random.Next(s.Length)]).ToArray());
            return result;
        }
    }
}
