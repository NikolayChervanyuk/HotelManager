namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        SureName = c.String(),
                        Number = c.String(),
                        Email = c.String(),
                        Adult = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoomNumber = c.String(),
                        User = c.String(),
                        AccomodationDate = c.DateTime(nullable: false),
                        DischargeDate = c.DateTime(nullable: false),
                        BreakFastIncluded = c.Boolean(nullable: false),
                        Allinclusive = c.Boolean(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Capacity = c.Int(nullable: false),
                        Occupation = c.Boolean(nullable: false),
                        AdultBedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChildBedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Number = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        MiddleName = c.String(),
                        SureName = c.String(),
                        IDnum = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Assignment = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Rooms");
            DropTable("dbo.Reservations");
            DropTable("dbo.Clients");
        }
    }
}
