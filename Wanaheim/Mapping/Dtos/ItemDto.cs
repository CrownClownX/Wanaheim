using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wanaheim.Mapping.Dtos
{
    public class ItemDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
        
        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int SubcategoryId { get; set; }

        public ItemDto()
        {
        }
    }
}
