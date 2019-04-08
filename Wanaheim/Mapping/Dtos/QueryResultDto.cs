using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wanaheim.Mapping.Dtos
{
    public class QueryResultDto<T>
    {
        public int Count { get; set; }
        public IEnumerable<T> Entities { get; set; }
    }
}
