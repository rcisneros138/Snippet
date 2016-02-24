namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class collectionsadded2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.snippetCollections",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Snippet_snippetCollection",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        collection_ID = c.Int(),
                        snippet_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.snippetCollections", t => t.collection_ID)
                .ForeignKey("dbo.Snippets", t => t.snippet_ID)
                .Index(t => t.collection_ID)
                .Index(t => t.snippet_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Snippet_snippetCollection", "snippet_ID", "dbo.Snippets");
            DropForeignKey("dbo.Snippet_snippetCollection", "collection_ID", "dbo.snippetCollections");
            DropIndex("dbo.Snippet_snippetCollection", new[] { "snippet_ID" });
            DropIndex("dbo.Snippet_snippetCollection", new[] { "collection_ID" });
            DropTable("dbo.Snippet_snippetCollection");
            DropTable("dbo.snippetCollections");
        }
    }
}
