namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notifaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifiications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Desc = c.String(nullable: false),
                        Created_at = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Vendor_Id = c.String(nullable: false),
                        Stutes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Notifiications");
        }
    }
}
