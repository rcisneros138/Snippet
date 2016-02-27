namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boolitup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.snippetCollections", "IsPublic", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.snippetCollections", "IsPublic");
        }
    }
}
