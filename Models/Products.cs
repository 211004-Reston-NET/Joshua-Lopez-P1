using System;

namespace Models
{
    public class Products
    {
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public string Description { get; set; }
        public string Category { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}\tProduct Name: {_name}\t Price $: {_price}\n";
        }
        public decimal PriceGrab()
        {
            return _price;
        }

    }
}