using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data;

namespace ELand.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public ApplicationUser() {
         this.Properties = new HashSet<Property>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public bool IsAgencyAdmin {get; set;}
        public int? AccountTypeId { get; set; }
        [ForeignKey("AccountTypeId")]
        public AccountType AccountType { get; set; }
        public int? AgencyId { get; set; }
        [ForeignKey("AgencyId")]
        public Agency Agency { get; set; }

        public virtual ICollection<Property> Properties { get; set; }


    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
          //  Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());

        }
        public DbSet<Agency> Agency { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<AccountType> AccountType { get; set; }
        public DbSet<AreaUnit> AreaUnit { get; set; }
        public DbSet<Purpose> Purpose { get; set; }
        public DbSet<PType> PType { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Faq> Faq { get; set; }
        public static ApplicationDbContext Create()
        {

            return new ApplicationDbContext();
        }

       // public System.Data.Entity.DbSet<ELand.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}