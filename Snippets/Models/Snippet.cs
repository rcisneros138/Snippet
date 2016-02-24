using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snippets.Models
{
    public class Snippet
    {
        public int ID { get; set; }
        public string SubmitterUserId { get; set; }
        public string Link { get; set; }
        public string description { get; set; }
    }
}