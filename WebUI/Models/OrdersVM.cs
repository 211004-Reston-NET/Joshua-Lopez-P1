using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class OrdersVM
    {
        public OrdersVM(Orders facility)
        {
            this.Id = facility.OrderId;
            this.CustomerId = facility.CustomerId;
            this.StoreFrontId=facility.StoreId;
            this.TotalPrice=facility.Total;
           
        }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int StoreFrontId { get; set; }
        public decimal TotalPrice {get;set;}
        

    }
}