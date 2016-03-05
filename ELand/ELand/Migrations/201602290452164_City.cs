namespace ELand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class City : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Properties", "CityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Properties", "CityId");
            AddForeignKey("dbo.Properties", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Properties", "CityId", "dbo.Cities");
            DropIndex("dbo.Properties", new[] { "CityId" });
            DropColumn("dbo.Properties", "CityId");
            DropTable("dbo.Cities");
        }
    }
}
