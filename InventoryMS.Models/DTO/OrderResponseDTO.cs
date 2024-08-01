using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMS.Models.DTO
{
    public class OrderResponseDTO
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime SaleDate { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } 

    }
}
