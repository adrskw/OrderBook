using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderBook.Web.Models;
using OrderBook.Web.Models.DataTables;
using OrderBook.Web.Repositories;

namespace OrderBook.Web.Utilities
{
    public static class DataTablesHelper
    {
        public static IEnumerable<Order> OrdersTable_Search(IOrderRepository orderRepository,
            DataTablesOrdersTableAjaxPost model,
            out int filteredResultsCount,
            out int totalResultsCount)
        {
            string searchValue = model.Search?.Value;
            int limit = model.Length;
            int skip = model.Start;
            string sortBy = "";
            bool isSortedAsc = true;
            OrderStatus? orderStatus = null;

            if (Enum.TryParse(model.StatusOfListedOrders, ignoreCase: true, out OrderStatus tempStatus)
                && Enum.IsDefined(typeof(OrderStatus), model.StatusOfListedOrders))
            {
                orderStatus = tempStatus;
            }

            if (model.Order != null)
            {
                sortBy = model.Columns[model.Order[0].Column].Data;
                isSortedAsc = model.Order[0].Dir.ToLower() == "asc";
            }

            var result = orderRepository.FindAll(searchValue, orderStatus, limit, skip, sortBy, isSortedAsc,
                out filteredResultsCount, out totalResultsCount);

            if (result == null)
            {
                return new List<Order>();
            }

            return result;
        }
    }
}