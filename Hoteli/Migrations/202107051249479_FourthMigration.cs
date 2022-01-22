namespace Hoteli.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourthMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Hotels", "LanacHotela_Id", "dbo.LanacHotelas");
            DropIndex("dbo.Hotels", new[] { "LanacHotela_Id" });
            RenameColumn(table: "dbo.Hotels", name: "LanacHotela_Id", newName: "LanacHotelaId");
            AlterColumn("dbo.Hotels", "LanacHotelaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Hotels", "LanacHotelaId");
            AddForeignKey("dbo.Hotels", "LanacHotelaId", "dbo.LanacHotelas", "Id", cascadeDelete: true);
            DropColumn("dbo.Hotels", "LanacHotelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Hotels", "LanacHotelId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Hotels", "LanacHotelaId", "dbo.LanacHotelas");
            DropIndex("dbo.Hotels", new[] { "LanacHotelaId" });
            AlterColumn("dbo.Hotels", "LanacHotelaId", c => c.Int());
            RenameColumn(table: "dbo.Hotels", name: "LanacHotelaId", newName: "LanacHotela_Id");
            CreateIndex("dbo.Hotels", "LanacHotela_Id");
            AddForeignKey("dbo.Hotels", "LanacHotela_Id", "dbo.LanacHotelas", "Id");
        }
    }
}
