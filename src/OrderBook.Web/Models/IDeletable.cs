using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Web.Models
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }
        ApplicationUser DeletedBy { get; set; }
    }
}