using System.ComponentModel.DataAnnotations;

namespace ETicaretKatmanlıMimariUI.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "İsim gereklidir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyisim gereklidir.")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı gereklidir.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "E-posta gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
    }
}
