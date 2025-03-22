using Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace ETicaretKatmanlıMimariUI.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Sipariş Numarası")]
        public string OrderNumber { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Display(Name = "Toplam Tutar")]
        public decimal Total { get; set; }

        [Display(Name = "Sipariş Durumu")]
        public EnumOrderState orderState { get; set; }

        // İstersen ek alanlar: Adress, City vs.
    }
}
