namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wallet2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Wallets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        my_balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        currunce = c.String(),
                        withdrawn_balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        user_id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.user_id, cascadeDelete: true)
                .Index(t => t.user_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wallets", "user_id", "dbo.User");
            DropIndex("dbo.Wallets", new[] { "user_id" });
            DropTable("dbo.Wallets");
        }
    }
}
