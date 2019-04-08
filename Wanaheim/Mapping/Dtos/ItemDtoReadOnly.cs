using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Wanaheim.Mapping.Dtos
{
    public class ItemDtoReadOnly
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public KeyValuePairDto Category { get; set; }
        public KeyValuePairDto Subcategory { get; set; }

        public ItemDtoReadOnly()
        {
        }
    }
}
