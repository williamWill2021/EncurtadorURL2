using EncurtadorURL.API.Backend.DataAccess.Context;
using EncurtadorURL.API.Backend.Interfaces.IDataAccess;
using EncurtadorURL.API.Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Policy;

namespace EncurtadorURL.API.Backend.DataAccess
{
    public class UrlEncurtadaDataAccess : IUrlEncurtadaDataAccess
    {
        private readonly IUrlEncurtadasContext _context;
        public UrlEncurtadaDataAccess(IUrlEncurtadasContext context)
        {
            _context = context;
        }

        public async Task<UrlEncurtadas> Create(UrlEncurtadas instance)
        {
            if (instance == null)
                throw new ApplicationException("Dados da instance url não informado.");
            if (string.IsNullOrEmpty(instance.Url))
                throw new ApplicationException("Url não informada.");

            var consulta = this.ReadBy_Url(instance.Url);
            if (consulta != null)
                return consulta;

            _context.Add(instance);
            int itensSalvos = await _context.SaveChangesAsync();
            if (itensSalvos <= 0)
                throw new ApplicationException("Não foi possível gravar os dados.");

            consulta = this.ReadBy_Url(instance.Url);
            instance.Id = consulta?.Id;

            return instance;
        }
        public async Task<UrlEncurtadas> Update(UrlEncurtadas instance)
        {
            if (instance == null)
                throw new ApplicationException("Dados da instance url não informado.");

            _context.UrlEncurtadas.Update(instance);
            int itensSalvos = await _context.SaveChangesAsync();
            if (itensSalvos <= 0)
                throw new ApplicationException("Não foi possível atualizar os dados.");

            return instance;
        }
        public bool VerificarUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ApplicationException("URL para encurtar não informada.");

            bool result = false;
            WebRequest request = WebRequest.Create(url);

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                    result = true;
            }
            catch(Exception ex) { }

            return result;
        }
        public UrlEncurtadas? ReadBy_ID(long? id)
        {
            if (id == null)
                throw new ApplicationException("ID para busca não informado.");

            return _context.UrlEncurtadas.FirstOrDefault(m => m.Id == id);
        }        
        public UrlEncurtadas? ReadBy_Url(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ApplicationException("URL para busca não informada.");

            return _context.UrlEncurtadas.FirstOrDefault(m => m.Url == url);

        }
        public UrlEncurtadas? ReadBy_ShortUrl(string shortUrl)
        {
            if (string.IsNullOrEmpty(shortUrl))
                throw new ApplicationException("Short URL para busca não informada.");

            return _context.UrlEncurtadas.FirstOrDefault(m => m.ShortUrl == shortUrl);
        }
        public async Task<UrlEncurtadas?> ReadBy_IdShortUrl(string idShortUrl)
        {
            if (string.IsNullOrEmpty(idShortUrl))
                throw new ApplicationException("IdShort URL para busca não informada.");

            return _context.UrlEncurtadas.FirstOrDefault(m => m.IdShortUrl == idShortUrl);
        }

    }
}
