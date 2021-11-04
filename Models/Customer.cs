using System;
using System.Collections.Generic;

namespace Models
{
    public class Customer
    {
        //Created to print out name of corresponding customer on each menu when succesfully signed in


        //fields

         private string _name;
         private string _address;
         private string _contact;
        private List<Orders> customerOrders = new List<Orders>();

        private string _username;
        private string _password;
        public int _age { get; set; }
        private decimal _currency;
        public int Id { get; set; }
        public string Position { get; set; }
        public decimal Currency
        {
            get { return decimal.Round(_currency,2); }
            set { _currency = decimal.Round(value,2); }
        }

        //constructors
        public Customer(string name, string address, string contact,string user, string pass){

        }
        public Customer(){}

        //in the JSON file the information would be entered in the way the constructors are for each property
        //property that uses the field name
        public string CustomerName
        {
            get { return _name; }
            set { _name = value; }
        }
        

       
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        
        public string Contact
        {
            get { return _contact; }
            set { _contact = value; }
        }

        
        public List<Orders> MyOrders
            {
                set { customerOrders = value; }
                get { return customerOrders; }
            }
        

        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
    
    public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        //in order to simply print out a customers information completely in a writeline
        public override string ToString()
        {
            return $"Name: {_name}\tAddress: {_address}\tContact: {_contact}";
        }
        public  string PrintName()
        {
            return _name;
        }

    
    }
}