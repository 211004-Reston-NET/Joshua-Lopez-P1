using System;

namespace Models
{
    public class LineItems
    {
        public int StoreID { get; set; }
        public int ProductID { get; set; }

        private Products _product;
        public Products ProductEstablish
        {
            get { return _product; }
            set { _product = value; }
        }


        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public override string ToString()
        {
            return $"{_product}\tquantity available : {_quantity}\n";
        }

        public int AmountGrab()
        {
            return _quantity;
        }

        public  Products Product_obj { get; set; }
        public  StoreFront Store_obj { get; set; }
    }
}