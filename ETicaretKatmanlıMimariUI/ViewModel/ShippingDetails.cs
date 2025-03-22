using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretKatmanlıMimariUI.ViewModels
{
    public class ShippingDetails
    {
        [Required(ErrorMessage ="Boş geçmeyiniz")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Boş geçmeyiniz")]
        public string AdressTitle { get; set; }
        [Required(ErrorMessage = "Boş geçmeyiniz")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "Boş geçmeyiniz")]
        public string City { get; set; }
    }
}
