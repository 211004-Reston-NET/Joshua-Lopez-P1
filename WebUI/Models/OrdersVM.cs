using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class OrdersVM
    {
        public OrdersVM() { }
        public OrdersVM(Orders facility)
        {
            this.Id = facility.OrderId;
            this.CustomerId = facility.CustomerId;
            this.StoreFrontId=facility.StoreId;
            this.TotalPrice=facility.Total;
            //idk how this worked
            foreach(LineItems x in facility.ItemsList)
            {
                this.ItemId=x.ProductEstablish.Id;
                this.ItemName=x.ProductEstablish.Name;
                this.Quantity=x.Quantity;

            }
            
        }
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int StoreFrontId { get; set; }
        [Required]
        public decimal TotalPrice {get;set;}
        public string  ItemName {get;set;}
        public int Quantity{get;set;}
        public int ItemId{get;set;}
        
        

    }
}