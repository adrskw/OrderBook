using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OrderBook.Web.Models.DataTables
{
    public class DataTablesSearch
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("regex")]
        public string Regex { get; set; }
    }
}