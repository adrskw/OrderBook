using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using OrderBook.Web.Models;
using OrderBook.Web.Utilities;

namespace OrderBook.Web.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return context.Products.Include(x => x.Category).ToList();
        }

        public Product GetById(int id)
        {
            return context.Products
                            .Include(x => x.Category)
                            .FirstOrDefault(x => x.Id == id);
        }

        public void Add(Product product)
        {
            context.Products.Add(product);
        }

        public void Update(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var product = context.Products.Find(id);

            context.Products.Remove(product);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}