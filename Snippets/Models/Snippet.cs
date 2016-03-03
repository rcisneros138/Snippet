using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snippets.Models
{
    public class Snippet
    {
        public int ID { get; set; }
        public string SubmitterUserId { get; set; }

        [Required]
        [Url(ErrorMessage ="Enter a valid URL")]

        public string Link { get; set; }
        public string description { get; set; }
        public byte[] image { get; set; }
        public virtual snippetCollection  SnippetCollection{ get; set; }
    }
}