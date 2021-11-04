using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLogic.Entities
{
    public partial class OrdersRecord
    {
        public OrdersRecord()
        {
            OrderHistories = new HashSet<OrderHistory>();
        }

        public int OrderId { get; set; }
        public decimal Total { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual StoreFront Store { get; set; }
        public virtual ICollection<OrderHistory> OrderHistories { get; set; }
    }
}
