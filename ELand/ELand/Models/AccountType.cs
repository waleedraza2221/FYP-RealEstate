using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELand.Models
{
    public class AccountType
    {
        public AccountType() { 
        
        this.Users=new HashSet<ApplicationUser>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}