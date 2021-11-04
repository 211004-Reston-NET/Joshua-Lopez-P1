using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLogic.Entities
{
    public partial class OrderHistory
    {
        public int ReferenceId { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int LineQuantity { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual OrdersRecord Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual StoreFront Store { get; set; }
    }
}
