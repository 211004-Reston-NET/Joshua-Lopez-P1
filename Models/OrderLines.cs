using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OrderLines
    {
        public int ReferenceId { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int LineQuantity { get; set; }

        public  Customer Customer_obj { get; set; }
        public  Orders Order_obj { get; set; }
        public  Products Product_obj { get; set; }
        public  StoreFront Store_obj { get; set; }
    }
}
