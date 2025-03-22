using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ETicaretKatmanlıMimariUI.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsHome { get; set; }
        public bool IsApprovew { get; set; }
        // Kategori bilgisi
        public int CategoryId { get; set; }
        [BindNever]
        public string CategoryName { get; set; }
        // İhtiyaca göre ek alanlar:
        public int Stock { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [BindNever]
        public SelectList CategoryList { get; set; }
    }
}