﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELand.Models
{
    public class Type
    {
        public Type() { 
        
        this.Properties=new HashSet<Property>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}