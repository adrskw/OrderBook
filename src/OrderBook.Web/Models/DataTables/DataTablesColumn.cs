using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OrderBook.Web.Models.DataTables
{
    public class DataTablesColumn
    {
        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("searchable")]
        public bool Searchable { get; set; }

        [JsonProperty("orderable")]
        public bool Orderable { get; set; }

        [JsonProperty("search")]
        public DataTablesSearch Search { get; set; }
    }
}