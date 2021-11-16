using System;
using System.Collections.Generic;
using Models;


namespace Models
{
    public class Orders
    {
        public int OrderId { get; set; }
        public decimal Total { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }

        public virtual Customer Customer_obj { get; set; }
        public virtual StoreFront Store_obj { get; set; }
        public virtual ICollection<OrderLines> OrderHistories { get; set; }






        
    }
}