using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Wanaheim.Mapping.Dtos
{
    public class CategoryDto
    {
        public CategoryDto()
        {
           Subcategories = new Collection<KeyValuePairDto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<KeyValuePairDto> Subcategories { get; set; }
    }
}
