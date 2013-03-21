using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class Feedback
    {
        public int? Id { get; set; }
        [Required]
        [MaxLength(140)]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string TwitterHandle { get; set; }
        public string Comment { get; set; }

    }
}