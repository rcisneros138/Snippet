namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ihatemylife : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Snippets", name: "snippetCollection_ID", newName: "SnipperCollection_ID");
            RenameIndex(table: "dbo.Snippets", name: "IX_snippetCollection_ID", newName: "IX_SnipperCollection_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Snippets", name: "IX_SnipperCollection_ID", newName: "IX_snippetCollection_ID");
            RenameColumn(table: "dbo.Snippets", name: "SnipperCollection_ID", newName: "snippetCollection_ID");
        }
    }
}
