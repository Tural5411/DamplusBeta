using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.DTOs
{
    public class UserPasswordChangeDto
    {
        [DisplayName("Hal-hazırdakı Şifrəniz")]
        [Required(ErrorMessage = "{0} boş olmamalıdır")]
        [MaxLength(30, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(5, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [DisplayName("Yeni Şifrəniz")]
        [Required(ErrorMessage = "{0} boş olmamalıdır")]
        [MaxLength(30, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(5, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DisplayName("Yeni Şifrəniziz təkrarı")]
        [Required(ErrorMessage = "{0} boş olmamalıdır")]
        [MaxLength(30, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(5, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Şifrələr bir-biri ilə eyni olmalıdır!")]
        public string RepeatPassword { get; set; }
    }
}
