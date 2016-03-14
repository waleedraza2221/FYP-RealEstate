using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELand.Models.ViewModels
{
    public class ViewProperty
    {
        public int Id { get; set; }
  
        public string Title { get; set; }

        public string Description { get; set; }
      
        public Int64 Price { get; set; }

        public string GlobalId { get; set; }
 
        public string City { get; set; }

        public string MainImage { get; set; }
        


    }
}