using Dukkantek.Inventory.Domain.Categories;
using Dukkantek.Inventory.Domain.Products;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Inventory.Application.Interfaces.Persistence
{
    public interface IAppDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
