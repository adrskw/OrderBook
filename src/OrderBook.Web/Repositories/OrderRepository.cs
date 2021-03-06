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
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return context.Orders
                            .Include(o => o.Customer)
                                .ThenInclude(c => c.Address)
                            .Include(o => o.Customer)
                                .ThenInclude(c => c.Invoice)
                            .Include(o => o.Products)
                                .ThenInclude(d => d.Product)
                                    .ThenInclude(p => p.Category)
                            .Include(o => o.DeliveryMethod)
                            .Include(o => o.SelectedDeliveryAddress)
                                .ThenInclude(a => a.PickupPoint)
                            .Include(o => o.Payment)
                            .Include(o => o.AllegroOrderData)
                            .ToList();
        }

        public Order GetById(int id)
        {
            return context.Orders
                            .Include(o => o.Customer)
                                .ThenInclude(c => c.Address)
                            .Include(o => o.Customer)
                                .ThenInclude(c => c.Invoice)
                            .Include(o => o.Products)
                                .ThenInclude(d => d.Product)
                                    .ThenInclude(p => p.Category)
                            .Include(o => o.DeliveryMethod)
                            .Include(o => o.SelectedDeliveryAddress)
                                .ThenInclude(a => a.PickupPoint)
                            .Include(o => o.Payment)
                            .Include(o => o.AllegroOrderData)
                            .SingleOrDefault(o => o.Id == id);
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

        public IEnumerable<Order> FindAll(string searchValue, OrderStatus? orderStatus, int limit, int skip,
            string sortBy, bool isSortedAsc, out int filteredResultsCount, out int totalResultsCount)
        {
            var whereClause = LinqQueryHelper.OrderSearch_BuildDynamicWhereClause(searchValue, orderStatus);

            if (string.IsNullOrEmpty(searchValue))
            {
                sortBy = "Id";
                isSortedAsc = true;
            }

            // create sorting string
            sortBy += (isSortedAsc) ? " asc" : " desc";

            var result = context.Orders
                            .Include(o => o.Customer)
                                .ThenInclude(c => c.Address)
                            .Include(o => o.Customer)
                                .ThenInclude(c => c.Invoice)
                            .Include(o => o.Products)
                                .ThenInclude(d => d.Product)
                                    .ThenInclude(p => p.Category)
                            .Include(o => o.DeliveryMethod)
                            .Include(o => o.SelectedDeliveryAddress)
                                .ThenInclude(a => a.PickupPoint)
                            .Include(o => o.Payment)
                            .Include(o => o.AllegroOrderData)
                            .AsExpandable()
                            .Where(whereClause)
                            .OrderBy(sortBy)
                            .Skip(skip)
                            .Take(limit)
                            .ToList();
            //.Select(o => new Order()
            // {
            //     Id = o.Id,
            //     Type = o.Type,
            //     Customer = new Customer()
            //     {
            //     },
            //     Products = o.Products,
            //     DeliveryMethod = o.DeliveryMethod,
            //     SelectedDeliveryAddress = o.SelectedDeliveryAddress,
            //     NumberOfPackages = o.NumberOfPackages,
            //     Payment = o.Payment,
            //     TotalToPay = o.TotalToPay,
            //     IsInvoiceRequired = o.IsInvoiceRequired,
            //     Status = o.Status,
            //     OrderDate = o.OrderDate,
            //     SentDate = o.SentDate,
            //     Note = o.Note,
            //     AllegroOrderData = o.AllegroOrderData
            // })
            filteredResultsCount = context.Orders.AsExpandable().Where(whereClause).Count();
            totalResultsCount = context.Orders.Count();

            return result;
        }
    }
}