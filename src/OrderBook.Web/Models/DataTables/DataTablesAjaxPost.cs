using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OrderBook.Web.Models.DataTables
{
    public class DataTablesAjaxPost
    {
        [JsonProperty("draw")]
        public int Draw { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("columns")]
        public List<DataTablesColumn> Columns { get; set; }

        [JsonProperty("search")]
        public DataTablesSearch Search { get; set; }

        [JsonProperty("order")]
        public List<DataTablesOrder> Order { get; set; }
    }
}