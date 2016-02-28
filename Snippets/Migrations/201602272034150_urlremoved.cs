namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class urlremoved : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Snippets", "Link", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Snippets", "Link", c => c.String(nullable: false));
        }
    }
}
