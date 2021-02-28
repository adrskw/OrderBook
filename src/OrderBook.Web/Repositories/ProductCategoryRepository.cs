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
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ApplicationDbContext context;

        public ProductCategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<ProductCategory> GetAllProductCategories()
        {
            return context.ProductCategories.ToList();
        }

        public ProductCategory GetById(int id)
        {
            return context.ProductCategories.Find(id);
        }

        public void Add(ProductCategory productCategory)
        {
            context.ProductCategories.Add(productCategory);
        }

        public void Update(ProductCategory productCategory)
        {
            context.Entry(productCategory).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var productCategory = context.ProductCategories.Find(id);

            context.ProductCategories.Remove(productCategory);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}