namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservationMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "isforsave", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "isforsave");
        }
    }
}
