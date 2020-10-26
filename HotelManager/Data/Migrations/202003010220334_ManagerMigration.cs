namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManagerMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Reservation_ID", c => c.Int());
            CreateIndex("dbo.Clients", "Reservation_ID");
            AddForeignKey("dbo.Clients", "Reservation_ID", "dbo.Reservations", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "Reservation_ID", "dbo.Reservations");
            DropIndex("dbo.Clients", new[] { "Reservation_ID" });
            DropColumn("dbo.Clients", "Reservation_ID");
        }
    }
}
