using System.ComponentModel.DataAnnotations;

namespace Entities.Enum
{
    public enum EnumOrderState
    {
        [Display(Name = "Onay Bekliyor")]
        Waiting,
        [Display(Name = "Tamamlandı")]
        Completed
    }
}