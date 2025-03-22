using System.ComponentModel.DataAnnotations;

namespace ETicaretKatmanlıMimariUI.ViewModels
{
    public enum EnumOrderState
    {
        [Display(Name = "Onay Bekliyor")]
        Waiting,
        [Display(Name = "Tamamlandı")]
        Completed
    }
}
