using EncurtadorURL.API.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace EncurtadorURL.API.Backend.Interfaces.IDataAccess
{
    public interface IUrlEncurtadaDataAccess
    {
        Task<UrlEncurtadas> Create(UrlEncurtadas instance);
        Task<UrlEncurtadas> Update(UrlEncurtadas instance);
        public UrlEncurtadas? ReadBy_ID(long? id);
        public UrlEncurtadas? ReadBy_Url(string url);
        UrlEncurtadas? ReadBy_ShortUrl(string shortUrl);
        Task<UrlEncurtadas?> ReadBy_IdShortUrl(string idShortUrl);
        bool VerificarUrl(string url);
    }
}
