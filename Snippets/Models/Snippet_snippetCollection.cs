using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snippets.Models
{
    public class Snippet_snippetCollection
    {
        public int ID { get; set; }
        public virtual Snippet snippet { get; set; }

        public virtual snippetCollection collection { get; set; }

    }
}