using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class LineItemVM
    {
        public LineItemVM()
        {

        }
        public LineItemVM(LineItems client)
        {

            this.Quantity = client.Quantity;
            this.item=client.ProductEstablish;
            this.ProductID=client.ProductID;
            this.StoreID=client.StoreID;
         
        }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public int StoreID { get; set; }
        [Required]
        public int ProductID { get; set; }

        public Products item{get;set;}
        
        


    }
}