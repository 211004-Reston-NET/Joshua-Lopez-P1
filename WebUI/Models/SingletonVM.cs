using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class SingletonVM
    {
        public SingletonVM()
        {
                
        }

       public static CustomerVM currentuser{get;set;}
      

    }
}