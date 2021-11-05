using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class CustomerVM
    {
        public CustomerVM(Customer client)
        {
            this.Id = client.Id;
            this.Name = client.Name;
            this.Address=client.Address;
            this.Contact=client.Email;
            this.UserName=client.UserName;
            this.Password=client.Password;
            this.Age=client.Age;
            this.Position=client.Category;
            this.Currency=client.CurrentCurrency;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Age {get;set;}
        public string Position{get;set;}
        public decimal Currency{get;set;}

    }
}