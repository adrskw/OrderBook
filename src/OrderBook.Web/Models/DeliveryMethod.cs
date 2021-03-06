﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Web.Models
{
    public enum DeliveryMethodCarrier
    {
        GLS,
        DHL,
        DPD,
        InpostPaczkomaty,
        PaczkaWRuchu
    }

    public class DeliveryMethod : IDeletable
    {
        public int Id { get; set; }

        [Required]
        public DeliveryMethodCarrier Carrier { get; set; }

        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public bool IsCashOnDelivery { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
    }
}