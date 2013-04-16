using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class CrowdFeedback
    {
            public int? Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Twitter { get; set; }
            public string Message { get; set; }
    }
}