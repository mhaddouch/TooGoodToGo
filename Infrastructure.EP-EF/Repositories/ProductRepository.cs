using Core.Domain;
using Core.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EP_EF.Repositories
{
    public class ProductRepository :IProductRepository
    {
        private readonly PackageDbContext _context;

        public ProductRepository(PackageDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }
    }
}
