namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActiveToSubCat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sub_Category", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sub_Category", "Active");
        }
    }
}
