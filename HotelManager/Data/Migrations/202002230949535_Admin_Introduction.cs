namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Admin_Introduction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Admins");
        }
    }
}
