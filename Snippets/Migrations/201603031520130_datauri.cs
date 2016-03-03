namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datauri : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Snippets", "image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Snippets", "image", c => c.Binary());
        }
    }
}
