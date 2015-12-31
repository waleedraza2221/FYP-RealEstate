using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELand.Models
{
    public class Agency
    {
        public Agency() {
            this.Users = new HashSet<ApplicationUser>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Skype { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
     }
}