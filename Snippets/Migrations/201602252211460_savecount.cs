namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class savecount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.snippetCollections", "SaveCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.snippetCollections", "SaveCount");
        }
    }
}
