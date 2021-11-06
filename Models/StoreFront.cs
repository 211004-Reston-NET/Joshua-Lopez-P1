using System.Collections.Generic;



namespace Models
{
    public class StoreFront
    {

        public int Id { get; set; }
        private string _name;
        public string StoreName
        {
            get { return _name; }
            set { _name = value; }
        }


        private string _address;
        public string Location
        {
            get { return _address; }
            set { _address = value; }
        }


        //private List<Orders> orderslist = new List<Orders>();
        //public List<Orders> EstablishOrders
        //{
        //    set { orderslist = value; }
        //    get { return orderslist; }
        //}
        // public List<Products> productslist = new List<Products>();
        // public List<Products> EstablishProducts
        //     {
        //         set { productslist= value; }
        //         get { return productslist; }
        //     }

        //private List<LineItems> _itemslist = new List<LineItems>();
        //public List<LineItems> Stock
        //{
        //    set { _itemslist = value; }
        //    get { return _itemslist; }
        //}

        public override string ToString()
        {
            return $"Id: {Id}\tStore Name : {_name}\tAddress: {_address}";
        }

        public List<OrderLines> orderline_ { get; set; }
        public  ICollection<Orders> OrdersRecords { get; set; }
        public  ICollection<LineItems> Stocks { get; set; }



    }
}