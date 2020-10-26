namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTable", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTable", "IsActive");
        }
    }
}
