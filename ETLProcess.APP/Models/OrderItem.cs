using System;
using System.Collections.Generic;

namespace ETLProcess.APP.Models
{
    public partial class OrderItem
    {
        public int Id { get; set; }
        public string ProductId { get; set; } = null!;
        public decimal Quantity { get; set; }
        public decimal OrderId { get; set; }

    }
}
