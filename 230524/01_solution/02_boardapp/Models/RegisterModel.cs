using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _02_boardapp.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "이메일 주소는 필수입니다.")]
        [EmailAddress]
        [DisplayName("이메일")]
        public string Email { get; set; }

        [DisplayName("핸드폰번호")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "패스워드는 필수입니다.")]
        [DataType(DataType.Password)]
        [DisplayName("패스워드")]
        public string Password { get; set; }

        [Required(ErrorMessage = "패스워드 확인은 필수입니다.")]
        [DataType(DataType.Password)]
        [DisplayName("패스워드 확인")]
        [Compare("Password", ErrorMessage = "패스워드 불일치!")]
        public string ConfirmPassword { get; set; }
    }
}
