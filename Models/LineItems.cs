using System;

namespace Models
{
    public class LineItems
    {
        public int StoreID { get; set; }
        public int ProductID { get; set; }


        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }


        public int AmountGrab()
        {
            return _quantity;
        }

        public  Products Product_obj { get; set; }
        public  StoreFront Store_obj { get; set; }
    }
}