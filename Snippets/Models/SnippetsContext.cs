using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Snippets.Models
{
    public class SnippetsContext : DbContext 
    {
        public SnippetsContext():base("DefaultConnection")
        {
            
        }
        public DbSet<Snippet> snippets { get; set; }
        public DbSet<snippetCollection> collections { get; set; }

        public DbSet<Snippet_snippetCollection> snippet_collections { get; set; }
    }
}