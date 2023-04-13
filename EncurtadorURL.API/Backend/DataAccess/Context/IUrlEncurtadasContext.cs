using EncurtadorURL.API.Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EncurtadorURL.API.Backend.DataAccess.Context
{
    public interface IUrlEncurtadasContext
    {
        DbSet<UrlEncurtadas> UrlEncurtadas { get; set; }
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry Add(object entity);
    }
}
