using EncurtadorURL.API.Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EncurtadorURL.API.Backend.DataAccess.Context
{
    public class UrlEncurtadasContext : DbContext, IUrlEncurtadasContext
    {
        public UrlEncurtadasContext(DbContextOptions<UrlEncurtadasContext> options)
            : base(options) { }

        public DbSet<UrlEncurtadas> UrlEncurtadas { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        public override EntityEntry Add(object entity)
        {
            return base.Add(entity);
        }

    }
}
