using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class StoreFrontVM
    {
        public StoreFrontVM(StoreFront facility)
        {
            this.Id = facility.Id;
            this.Name = facility.StoreName;
            this.Address=facility.Location;
           
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        

    }
}