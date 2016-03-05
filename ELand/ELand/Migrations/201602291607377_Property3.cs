namespace ELand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Property3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Properties", "PublishDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Properties", "UpdateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Properties", "UpdateDate");
            DropColumn("dbo.Properties", "PublishDate");
        }
    }
}
