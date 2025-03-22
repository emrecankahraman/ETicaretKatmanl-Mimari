using System.ComponentModel.DataAnnotations;

namespace ETicaretKatmanlıMimariUI.ViewModels
{
    public enum EnumOrderState
    {
        [Display(Name = "Onay Bekliyor")]
        Waiting=0,
        [Display(Name = "Tamamlandı")]
        Completed=1,
        [Display(Name = "Shipping")]
        Shipping = 2,

        [Display(Name = "Canceled")]
        Canceled = 3
    }
}
