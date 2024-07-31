﻿using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryMS.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime SaleDate { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
