using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderBook.Web.Models;

namespace OrderBook.Web.Repositories
{
    public interface IProductCategoryRepository
    {
        IEnumerable<ProductCategory> GetAllProductCategories();

        ProductCategory GetById(int id);

        void Add(ProductCategory productCategory);

        void Update(ProductCategory productCategory);

        void Delete(int id);

        void Save();
    }
}