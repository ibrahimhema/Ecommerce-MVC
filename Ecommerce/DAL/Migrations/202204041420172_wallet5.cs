namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wallet5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Wallets", "User_Id", "dbo.User");
            AddForeignKey("dbo.Wallets", "User_Id", "dbo.User", "Id", cascadeDelete: true);
            DropColumn("dbo.Wallets", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Wallets", "UserId", c => c.String(nullable: false));
            DropForeignKey("dbo.Wallets", "User_Id", "dbo.User");
            AddForeignKey("dbo.Wallets", "User_Id", "dbo.User", "Id");
        }
    }
}
