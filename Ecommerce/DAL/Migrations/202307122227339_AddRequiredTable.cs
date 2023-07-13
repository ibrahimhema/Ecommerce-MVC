namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductColors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ColorName = c.String(nullable: false),
                        ColorCode = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        PhotoURL = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductSizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SizeName = c.String(nullable: false),
                        SizePrice = c.Decimal(precision: 18, scale: 2),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductSizes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductColors", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductSizes", new[] { "ProductId" });
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            DropIndex("dbo.ProductColors", new[] { "ProductId" });
            DropTable("dbo.ProductSizes");
            DropTable("dbo.ProductImages");
            DropTable("dbo.ProductColors");
        }
    }
}
