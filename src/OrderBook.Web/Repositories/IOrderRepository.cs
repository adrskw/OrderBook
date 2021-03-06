using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderBook.Web.Models;

namespace OrderBook.Web.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();

        Order GetById(int id);

        void Add(Order order);

        void Update(Order order);

        void Delete(int id);

        void Save();

        IEnumerable<Order> FindAll(string searchValue, OrderStatus? orderStatus, int limit, int skip,
            string sortBy, bool isSortedAsc, out int filteredResultsCount, out int totalResultsCount);
    }
}