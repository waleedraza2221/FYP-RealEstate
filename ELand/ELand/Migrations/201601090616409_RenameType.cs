namespace ELand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameType : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Types", newName: "PTypes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PTypes", newName: "Types");
        }
    }
}
