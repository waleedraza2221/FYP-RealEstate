namespace ELand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agencies", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Agencies", "Image");
        }
    }
}
