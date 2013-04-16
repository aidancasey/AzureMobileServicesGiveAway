using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AidansWindowsStoreApp.Model
{
    public class Channel
    {
        public int Id { get; set; }


        [DataMember(Name = "uri")]
        public string Uri { get; set; }


    }
}
