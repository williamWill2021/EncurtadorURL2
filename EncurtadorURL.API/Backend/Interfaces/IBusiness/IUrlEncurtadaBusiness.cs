using EncurtadorURL.API.Backend.Models;

namespace EncurtadorURL.API.Backend.Interfaces.IBusiness
{
    public interface IUrlEncurtadaBusiness
    {
        Task<UrlEncurtadas> Create(UrlEncurtadas instance);
        Task<UrlEncurtadas> Update(UrlEncurtadas instance);
        UrlEncurtadas EncurtarUrl(string url);
        bool VerificarUrl(string url);
        UrlEncurtadas? ReadBy_ID(long? id);
        UrlEncurtadas? ReadBy_Url(string url);
        UrlEncurtadas? ReadBy_ShortUrl(string shortUrl);
        Task<UrlEncurtadas?> ReadBy_IdShortUrl(string idShortUrl);
        List<UrlEncurtadas> ObterTop5FromUrlsJson();
    }
}
