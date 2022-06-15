using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.DTOs
{
    public class EmailSendDto
    {
        [DisplayName("Adınız")]
        [Required(ErrorMessage ="{0} sahəsi boşdur")]
        [MaxLength(60,ErrorMessage ="{0} sahəhsi {1} dan böyük ola bilməz")]
        [MinLength(3,ErrorMessage ="{0} sahəhsi {1} dən kiçik ola bilməz")]
        public string Name { get; set; }
        [DisplayName("Email Ünvanınız")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} sahəsi boşdur")]
        [MaxLength(100, ErrorMessage = "{0} sahəhsi {1} dan böyük ola bilməz")]
        [MinLength(10, ErrorMessage = "{0} sahəhsi {1} dən kiçik ola bilməz")]
        public string Email { get; set; }
        [DisplayName("Başlıq")]
        [Required(ErrorMessage = "{0} sahəsi boşdur")]
        [MaxLength(120, ErrorMessage = "{0} sahəhsi {1} dan böyük ola bilməz")]
        [MinLength(2, ErrorMessage = "{0} sahəhsi {1} dən kiçik ola bilməz")]
        public string Subject { get; set; }
        [DisplayName("Mesajınız")]
        [Required(ErrorMessage = "{0} sahəsi boşdur")]
        [MaxLength(1000, ErrorMessage = "{0} sahəhsi {1} dan böyük ola bilməz")]
        [MinLength(10, ErrorMessage = "{0} sahəhsi {1} dən kiçik ola bilməz")]
        public string Message { get; set; }
    }
}
