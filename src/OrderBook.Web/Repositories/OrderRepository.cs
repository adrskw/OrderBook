using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderBook.Web.Models;

namespace OrderBook.Web.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return context.Orders.ToList();
        }

        public Order GetById(int id)
        {
            return context.Orders.Find(id);
        }

        public void Add(Order order)
        {
            context.Orders.Add(order);
        }

        public void Update(Order order)
        {
            context.Entry(order).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var order = context.Orders.Find(id);

            context.Orders.Remove(order);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}