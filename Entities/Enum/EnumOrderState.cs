using System.ComponentModel.DataAnnotations;

namespace Entities.Enum
{
    public enum EnumOrderState
    {
        [Display(Name = "Waiting")]
        Waiting=0,

        [Display(Name = "Completed")]
        Completed=1,

        [Display(Name = "Shipping")]
        Shipping=2,

        [Display(Name = "Canceled")]
        Canceled=3
    }
}
