using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OrderBook.Web.Models.DataTables
{
    public class DataTablesOrder
    {
        [JsonProperty("column")]
        public int Column { get; set; }

        [JsonProperty("dir")]
        public string Dir { get; set; }
    }
}