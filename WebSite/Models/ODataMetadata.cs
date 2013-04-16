using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class ODataMetadata<T> where T : class
    {
        private readonly long? _count;
        private IEnumerable<T> _results;

        public ODataMetadata(IEnumerable<T> results, long? count)
        {
            _count = count;
            _results = results;
        }

        public IEnumerable<T> Results
        {
            get { return _results; }
        }

        public long? Count
        {
            get { return _count; }
        }
    }
}