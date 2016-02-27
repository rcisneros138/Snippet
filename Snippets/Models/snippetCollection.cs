using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snippets.Models
{
    public class snippetCollection
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string SubmitterUserId { get; set; }

        public int SaveCount { get; set; }

        public bool IsPublic { get; set; }

        public virtual List<Snippet> snippets { get; set; }

        public snippetCollection()
        {
            snippets = new List<Snippet>();
        }



    }
}