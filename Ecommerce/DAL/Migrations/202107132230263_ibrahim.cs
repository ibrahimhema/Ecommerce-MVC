namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ibrahim : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Brands", "Photo", c => c.String());
            AlterColumn("dbo.Main_Category", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Main_Category", "Photo", c => c.String(nullable: false));
            AlterColumn("dbo.Brands", "Photo", c => c.String(nullable: false));
        }
    }
}
