using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.Concrete
{
    public class SmtpSettings
    {
        [DisplayName("Server")]
        [Required(ErrorMessage = "{0} sahəsi boş olmamalıdır.")]
        [MaxLength(100, ErrorMessage = "{0} sahəsi {1} karakterdən böyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} sahəsi {1} karakterdən kiçik olmamalıdır.")]
        public string Server { get; set; }
        [DisplayName("Port")]
        [Required(ErrorMessage = "{0} sahəsi boş olmamalıdır.")]
        [Range(0, 9999, ErrorMessage = "{0} sahesi en az {1}, en cox {2} deyerinde olmalıdır.")]
        public int Port { get; set; }
        [DisplayName("Göndərən Adı")]
        [Required(ErrorMessage = "{0} sahəsi boş olmamalıdır.")]
        [MaxLength(100, ErrorMessage = "{0} sahəsi {1} karakterdən böyük olmamalıdır.")]
        [MinLength(2, ErrorMessage = "{0} sahəsi {1} karakterdən kiçik olmamalıdır.")]
        public string SenderName { get; set; }
        [DisplayName("Göndərən Email")]
        [Required(ErrorMessage = "{0} sahəsi boş olmamalıdır.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} alanı e-posta formatında olmalıdır.")]
        [MaxLength(100, ErrorMessage = "{0} sahəsi {1} karakterdən böyük olmamalıdır.")]
        [MinLength(10, ErrorMessage = "{0} sahəsi {1} karakterdən kiçik olmamalıdır.")]
        public string SenderEmail { get; set; }
        [DisplayName("İstifadəçi Adı/E-Posta")]
        [Required(ErrorMessage = "{0} sahəsi boş olmamalıdır.")]
        [MaxLength(100, ErrorMessage = "{0} sahəsi {1} karakterdən böyük olmamalıdır.")]
        [MinLength(1, ErrorMessage = "{0} sahəsi {1} karakterdən kiçik olmamalıdır.")]
        public string Username { get; set; }
        [DisplayName("Şifrə")]
        [Required(ErrorMessage = "{0} sahəsi boş olmamalıdır.")]
        [DataType(DataType.Password, ErrorMessage = "{0} alanı şifre formatında olmalıdır.")]
        [MaxLength(50, ErrorMessage = "{0} sahəsi {1} karakterdən böyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} sahəsi {1} karakterdən kiçik olmamalıdır.")]
        public string Password { get; set; }
    }
}
