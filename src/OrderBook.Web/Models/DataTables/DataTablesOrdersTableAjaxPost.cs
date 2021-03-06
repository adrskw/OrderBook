using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OrderBook.Web.Models.DataTables
{
    public class DataTablesOrdersTableAjaxPost : DataTablesAjaxPost
    {
        [JsonProperty("statusOfListedOrders")]
        public string StatusOfListedOrders { get; set; }
    }
}