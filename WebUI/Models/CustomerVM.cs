using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class CustomerVM
    {
        public CustomerVM()
        {
                
        }
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
         [Required]
        public string Name { get; set; }
         [Required]
        public string Address { get; set; }
         [Required]
        public string Contact { get; set; }
         [Required]
        public string UserName { get; set; }
         [Required]
        public string Password { get; set; }
         [Required]
        public int Age {get;set;}
         [Required]
        public string Position{get;set;}
         [Required]
        public decimal Currency{get;set;}

        public override string ToString()
        {
            return $"Name: {Name}\tAddress: {Address}\tContact: {Contact}";
        }

    }
}