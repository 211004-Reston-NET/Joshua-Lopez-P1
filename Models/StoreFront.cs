using System.Collections.Generic;

using System;


namespace Models
{
    public class StoreFront
    {

        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }


        private List<Orders> orderslist = new List<Orders>();
        public List<Orders> EstablishOrders
        {
            set { orderslist = value; }
            get { return orderslist; }
        }
        // public List<Products> productslist = new List<Products>();
        // public List<Products> EstablishProducts
        //     {
        //         set { productslist= value; }
        //         get { return productslist; }
        //     }

        private List<LineItems> _itemslist = new List<LineItems>();
        public List<LineItems> Stock
        {
            set { _itemslist = value; }
            get { return _itemslist; }
        }

        public override string ToString()
        {
            return $"Id: {Id}\tStore Name : {_name}\tAddress: {_address}";
        }


    }
}