namespace ELand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "AccountTypeId", "dbo.AccountTypes");
            DropIndex("dbo.AspNetUsers", new[] { "AccountTypeId" });
            DropTable("dbo.AccountTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AccountTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.AspNetUsers", "AccountTypeId");
            AddForeignKey("dbo.AspNetUsers", "AccountTypeId", "dbo.AccountTypes", "Id");
        }
    }
}
