using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLogic.Entities
{
    public partial class Stock
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int InStock { get; set; }

        public virtual Product Product { get; set; }
        public virtual StoreFront Store { get; set; }
    }
}
