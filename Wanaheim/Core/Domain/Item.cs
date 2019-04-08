using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Wanaheim.Core.Domain
{
    public class Item 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }

        public Item()
        {
            Photos = new Collection<Photo>();
        }
    }
}
