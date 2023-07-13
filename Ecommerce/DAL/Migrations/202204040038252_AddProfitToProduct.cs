namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProfitToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Profit", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Profit");
        }
    }
}
