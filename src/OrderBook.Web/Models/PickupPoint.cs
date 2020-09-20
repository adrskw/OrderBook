using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Web.Models
{
    public enum PickupPointCarrier
    {
        PaczkaWRuchu,
        InpostPaczkomaty
    }

    public class PickupPoint
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public PickupPointCarrier Carrier { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string ZipCode { get; set; }
    }
}