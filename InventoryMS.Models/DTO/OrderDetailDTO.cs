using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMS.Models.DTO
{
    public class OrderDetailDto
    {
        public int ProductId { get; set; }  
        public int Quantity { get; set; }    
        public decimal UnitPrice { get; set; }  
    }
}
