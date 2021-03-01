using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderBook.Web.Models;

namespace OrderBook.Web.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();

        Product GetById(int id);

        void Add(Product product);

        void Update(Product product);

        void Delete(int id);

        void Save();
    }
}