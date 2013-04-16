using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        public string TwitterId { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }

    }
}