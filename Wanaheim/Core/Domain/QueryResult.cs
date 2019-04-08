using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wanaheim.Core.Domain
{
    public class QueryResult<T>
    {
        public int Count { get; set; }
        public IEnumerable<T> Entities { get; set; }
    }
}
