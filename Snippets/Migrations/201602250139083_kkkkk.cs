namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kkkkk : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Snippets", name: "SnipperCollection_ID", newName: "SnippetCollection_ID");
            RenameIndex(table: "dbo.Snippets", name: "IX_SnipperCollection_ID", newName: "IX_SnippetCollection_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Snippets", name: "IX_SnippetCollection_ID", newName: "IX_SnipperCollection_ID");
            RenameColumn(table: "dbo.Snippets", name: "SnippetCollection_ID", newName: "SnipperCollection_ID");
        }
    }
}
