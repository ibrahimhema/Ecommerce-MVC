namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wallet4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Wallets", "user_id", "dbo.User");
            DropIndex("dbo.Wallets", new[] { "user_id" });
            AddColumn("dbo.Wallets", "UserId", c => c.String(nullable: false));
            CreateIndex("dbo.Wallets", "User_Id");
            AddForeignKey("dbo.Wallets", "User_Id", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wallets", "User_Id", "dbo.User");
            DropIndex("dbo.Wallets", new[] { "User_Id" });
            DropColumn("dbo.Wallets", "UserId");
            CreateIndex("dbo.Wallets", "user_id");
            AddForeignKey("dbo.Wallets", "user_id", "dbo.User", "Id", cascadeDelete: true);
        }
    }
}
