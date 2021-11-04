using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLogic.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            OrderHistories = new HashSet<OrderHistory>();
            OrdersRecords = new HashSet<OrdersRecord>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Category { get; set; }
        public decimal CurrentCurrency { get; set; }

        public virtual ICollection<OrderHistory> OrderHistories { get; set; }
        public virtual ICollection<OrdersRecord> OrdersRecords { get; set; }
    }
}
