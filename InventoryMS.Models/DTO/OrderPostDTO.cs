﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMS.Models.DTO
{
    public class OrderPostDTO
    {
        public int CustomerId { get; set; }
        public List<OrderDetailResponseDTO> OrderDetails { get; set; }

    }
}
