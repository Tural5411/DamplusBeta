using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.DTOs
{
    public class UserLoginDto
    {
        [DisplayName("Email Ünvanı")]
        [Required(ErrorMessage = "{0} boş olmamalıdır")]
        [MaxLength(100, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(10, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Şifrə")]
        [Required(ErrorMessage = "{0} boş olmamalıdır")]
        [MaxLength(30, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(5, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Məni Xatırla")]
        public bool RememberMe { get; set; }
    }
}
