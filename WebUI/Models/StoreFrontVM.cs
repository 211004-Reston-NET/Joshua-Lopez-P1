using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class StoreFrontVM
    {

        public StoreFrontVM() { }
        public StoreFrontVM(StoreFront facility)
        {
            this.Id = facility.Id;
            this.Name = facility.StoreName;
            this.Address = facility.Location;

        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }


    }
}