using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Wanaheim.Core.Domain
{
    public class Category
    {
        public Category()
        {
            Subcategories = new Collection<Subcategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Subcategory> Subcategories { get; set; }
    }
}
