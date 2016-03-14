namespace ELand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class n : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Properties", "Price", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Properties", "Price", c => c.String(nullable: false));
        }
    }
}
