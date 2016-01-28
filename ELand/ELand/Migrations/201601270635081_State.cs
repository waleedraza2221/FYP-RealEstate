namespace ELand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class State : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Properties", "State", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Properties", "State");
        }
    }
}
