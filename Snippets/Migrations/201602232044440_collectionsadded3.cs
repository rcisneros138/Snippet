namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class collectionsadded3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.snippetCollections", "SubmitterUserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.snippetCollections", "SubmitterUserId");
        }
    }
}
