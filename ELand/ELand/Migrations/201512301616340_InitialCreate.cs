namespace ELand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Phone = c.String(),
                        Skype = c.String(),
                        Twitter = c.String(),
                        Instagram = c.String(),
                        IsAgencyAdmin = c.Boolean(nullable: false),
                        AccountTypeId = c.Int(),
                        AgencyId = c.Int(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountTypes", t => t.AccountTypeId)
                .ForeignKey("dbo.Agencies", t => t.AgencyId)
                .Index(t => t.AccountTypeId)
                .Index(t => t.AgencyId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Agencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Skype = c.String(),
                        Facebook = c.String(),
                        Twitter = c.String(),
                        Instagram = c.String(),
                        Email = c.String(),
                        Mobile = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                        Latitude = c.String(),
                        Longitude = c.String(),
                        MainImage = c.String(),
                        GalleryImages = c.String(),
                        Price = c.String(),
                        AreaID = c.Int(nullable: false),
                        TypeID = c.Int(nullable: false),
                        PurposeID = c.Int(nullable: false),
                        Bed = c.Int(nullable: false),
                        Bath = c.Int(nullable: false),
                        Kitchen = c.Int(nullable: false),
                        Garage = c.Int(nullable: false),
                        Pool = c.Int(nullable: false),
                        Air_Conditioning = c.Boolean(nullable: false),
                        Fridge = c.Boolean(nullable: false),
                        Kitchen_With_Island = c.Boolean(nullable: false),
                        ADSL_Cable = c.Boolean(nullable: false),
                        Grill = c.Boolean(nullable: false),
                        Exotic_Garden = c.Boolean(nullable: false),
                        WiFi = c.Boolean(nullable: false),
                        Full_HD_TV = c.Boolean(nullable: false),
                        Guest_House = c.Boolean(nullable: false),
                        HiFi_Audio = c.Boolean(nullable: false),
                        Digital_Antenna = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AreaUnits", t => t.AreaID, cascadeDelete: true)
                .ForeignKey("dbo.Purposes", t => t.PurposeID, cascadeDelete: true)
                .ForeignKey("dbo.Types", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.AreaID)
                .Index(t => t.TypeID)
                .Index(t => t.PurposeID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AreaUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Purposes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Properties", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Properties", "TypeID", "dbo.Types");
            DropForeignKey("dbo.Properties", "PurposeID", "dbo.Purposes");
            DropForeignKey("dbo.Properties", "AreaID", "dbo.AreaUnits");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "AgencyId", "dbo.Agencies");
            DropForeignKey("dbo.AspNetUsers", "AccountTypeId", "dbo.AccountTypes");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Properties", new[] { "UserId" });
            DropIndex("dbo.Properties", new[] { "PurposeID" });
            DropIndex("dbo.Properties", new[] { "TypeID" });
            DropIndex("dbo.Properties", new[] { "AreaID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "AgencyId" });
            DropIndex("dbo.AspNetUsers", new[] { "AccountTypeId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Types");
            DropTable("dbo.Purposes");
            DropTable("dbo.AreaUnits");
            DropTable("dbo.Properties");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Agencies");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AccountTypes");
        }
    }
}
