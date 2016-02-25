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

        public List<Snippet> snippets { get; set; }

        public snippetCollection()
        {
            snippets = new List<Snippet>();
        }



    }
}