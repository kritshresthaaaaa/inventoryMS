using InventoryMS.Models.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryMS.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
