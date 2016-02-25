namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Snippets", "snippetCollection_ID", c => c.Int());
            CreateIndex("dbo.Snippets", "snippetCollection_ID");
            AddForeignKey("dbo.Snippets", "snippetCollection_ID", "dbo.snippetCollections", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Snippets", "snippetCollection_ID", "dbo.snippetCollections");
            DropIndex("dbo.Snippets", new[] { "snippetCollection_ID" });
            DropColumn("dbo.Snippets", "snippetCollection_ID");
        }
    }
}
