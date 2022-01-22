namespace Hoteli.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigartion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        GodinaOsnivanja = c.Int(nullable: false),
                        BrojZaposlenih = c.Int(nullable: false),
                        BrojSoba = c.Int(nullable: false),
                        LanacHotelId = c.Int(nullable: false),
                        LanacHotela_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LanacHotelas", t => t.LanacHotela_Id)
                .Index(t => t.LanacHotela_Id);
            
            CreateTable(
                "dbo.LanacHotelas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        GodinaOsnivanja = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hotels", "LanacHotela_Id", "dbo.LanacHotelas");
            DropIndex("dbo.Hotels", new[] { "LanacHotela_Id" });
            DropTable("dbo.LanacHotelas");
            DropTable("dbo.Hotels");
        }
    }
}
