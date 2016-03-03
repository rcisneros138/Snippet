using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snippets.Models
{
    public class Collections_Snippet_CombinedModel
    {
        public IEnumerable<Snippet> snippets { get; set; }
        public Snippet snippet { get; set; }
        public string selectedSnippetID { get; set; }
        public List<SelectListItem> snippetDropdown { get; set; }

        public string url { get; set; }

        public string Image { get; set; }


        // collections below:

        public IEnumerable<snippetCollection> collection { get; set; }
        public string selectedCollectionID { get; set; }
        public List<SelectListItem> collectionDropdown { get; set; }

        // other

        public snippetCollection SnippetCollection { get; set; }
    }
}