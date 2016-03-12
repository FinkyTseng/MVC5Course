using System;
using System.Linq;
using System.Collections.Generic;

namespace MVC5Course.Models
{
    public class ProductRepository : EFRepository<Product>, IProductRepository
    {
        public override IQueryable<Product> All()
        {
            return base.All().Where(p => !p.IsDeleted);
        }

        public IQueryable<Product> All(bool isAll)
        {
            if (isAll)
            {
                return base.All();
            }
            else
            {
                return this.All();
            }
        }

        public Product Find(int Id)
        {
            return this.All().Where(p => p.ProductId == Id).FirstOrDefault();
        }
    }

    public interface IProductRepository : IRepository<Product>
    {

    }
}