namespace ELand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameType1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "Phone");
        }
    }
}
