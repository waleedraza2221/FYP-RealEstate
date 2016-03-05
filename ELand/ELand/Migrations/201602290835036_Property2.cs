namespace ELand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Property2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Properties", "GlobalId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Properties", "GlobalId");
        }
    }
}
