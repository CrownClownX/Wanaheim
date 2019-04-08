using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wanaheim.Mapping.Dtos
{
    public class ItemsQueryDto
    {
        public int? CategoryId { get; set; }
        public string SortBy { get; set; }
        public bool IfAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}
