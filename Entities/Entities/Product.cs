using ETicareBitirme.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public bool IsHome { get; set; } //Anasayfada mı
        public bool IsApprovew { get; set; } //satışta mı 

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
