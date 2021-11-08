using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class ProductsVM
    {
        public ProductsVM()
        {

        }
        public ProductsVM(Products client)
        {
            this.Id = client.Id;
            this.Name = client.Name;
            this.Category = client.Category;
            this.Description = client.Description;
            this.Price = client.Price;
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }
        [Required]
        public decimal Price { get; set; }


    }
}