namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConvertToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Profit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Profit", c => c.Int(nullable: false));
        }
    }
}
