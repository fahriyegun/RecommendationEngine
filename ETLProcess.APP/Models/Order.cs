using System;
using System.Collections.Generic;

namespace ETLProcess.APP.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public decimal OrderId { get; set; }
        public string UserId { get; set; } = null!;

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
