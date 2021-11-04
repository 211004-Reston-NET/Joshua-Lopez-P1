using System;
using System.Collections.Generic;
using Models;


namespace Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int StoreFrontId { get; set; }

        public  Customer Customer_obj { get; set; }
        public  StoreFront Store_obj { get; set; }
        public  List<OrderLines> orderline_ { get; set; }

        private List<LineItems> itemslist = new List<LineItems>();
        public List<LineItems> ItemsList
            {
                set { itemslist = value; }
                get { return itemslist; }
            }
        private StoreFront _location;
        public StoreFront Location
        {
            get { return _location; }
            set { _location = value; }
        }

        private decimal _totalprice;
        public decimal TotalPrice
        {
            get { return decimal.Round(_totalprice,2); }
            set { _totalprice = decimal.Round(value,2); }
        }


public override string ToString()
        {
            string text=$"Order is from location : {_location}\tTotal Price :${decimal.Round(_totalprice,2)}";
            return text;
        }
        
    }
}