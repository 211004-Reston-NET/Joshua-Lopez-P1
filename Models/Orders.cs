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






        // private List<LineItems> itemslist = new List<LineItems>();
        // public List<LineItems> ItemsList
        // {
        //    set { itemslist = value; }
        //    get { return itemslist; }
        // }
        // ////private StoreFront _location;
        // ////public StoreFront Location
        // ////{
        // ////    get { return _location; }
        // ////    set { _location = value; }
        // ////}

        


        //public override string ToString()
        //{
        //    string text = $"Order is from location : {_location}\tTotal Price :${decimal.Round(_totalprice, 2)}";
        //    return text;
        //}

    }
}