namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredTable2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Main_Category", "Avtive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Main_Category", "Avtive");
        }
    }
}
