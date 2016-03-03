namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Snippets", "image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Snippets", "image");
        }
    }
}
