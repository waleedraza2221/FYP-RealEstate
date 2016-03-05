namespace ELand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Property1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Properties", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Properties", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Properties", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Properties", "Latitude", c => c.String(nullable: false));
            AlterColumn("dbo.Properties", "Longitude", c => c.String(nullable: false));
            AlterColumn("dbo.Properties", "Area", c => c.String(nullable: false));
            AlterColumn("dbo.Properties", "Price", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Properties", "Price", c => c.String());
            AlterColumn("dbo.Properties", "Area", c => c.String());
            AlterColumn("dbo.Properties", "Longitude", c => c.String());
            AlterColumn("dbo.Properties", "Latitude", c => c.String());
            AlterColumn("dbo.Properties", "Address", c => c.String());
            AlterColumn("dbo.Properties", "Description", c => c.String());
            AlterColumn("dbo.Properties", "Title", c => c.String());
        }
    }
}
