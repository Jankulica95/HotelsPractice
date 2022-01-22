namespace Hoteli.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FifthMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Hotels", "Naziv", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.LanacHotelas", "Naziv", c => c.String(nullable: false, maxLength: 75));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LanacHotelas", "Naziv", c => c.String());
            AlterColumn("dbo.Hotels", "Naziv", c => c.String());
        }
    }
}
