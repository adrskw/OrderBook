using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqKit;
using OrderBook.Web.Models;

namespace OrderBook.Web.Utilities
{
    public static class LinqQueryHelper
    {
        public static Expression<Func<Order, bool>> OrderSearch_BuildDynamicWhereClause(string searchValue, OrderStatus? orderStatus = null)
        {
            // method to dynamically plugin a where clause
            var predicate = PredicateBuilder.New<Order>(true); // true -where(true) return all

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                var searchTerms = searchValue.Split(' ').ToList().ConvertAll(x => x.ToLower());

                foreach (var searchTerm in searchTerms)
                {
                    predicate = predicate.Or(o => o.Note.ToLower().Contains(searchTerm));
                }
            }

            if (orderStatus != null)
            {
                predicate = predicate.And(order => order.Status == orderStatus);
            }

            return predicate;
        }
    }
}