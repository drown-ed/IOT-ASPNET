﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace _02_boardapp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "이메일 주소는 필수입니다.")]
        [EmailAddress]
        [DisplayName("이메일")]
        public string Email { get; set; }

        [Required(ErrorMessage = "패스워드는 필수입니다.")]
        [DataType(DataType.Password)]
        [DisplayName("패스워드")]
        public string Password { get; set; }

        [DisplayName("메일 주소 기억")]
        public bool RememberMe { get; set; }   
    }
}
