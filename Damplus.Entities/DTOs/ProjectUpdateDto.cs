using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.DTOs
{
    public class ProjectUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Proyekt Adı")]
        [Required(ErrorMessage = "{0} adı boş ola bilməz!")] //{O} = Kateqoriya Adı
        [MaxLength(70, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")] //{1} = 70
        [MinLength(3, ErrorMessage = "{0} {1} - dən az ola bilməz!")] //{1} = 3
        public string Name { get; set; }
        [DisplayName("Müştəri Adı")]
        [Required(ErrorMessage = "{0} adı boş ola bilməz!")]
        [MaxLength(70, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(3, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string Client { get; set; }
        [DisplayName("Ərazi")]
        [Required(ErrorMessage = "{0} adı boş ola bilməz!")]
        [MaxLength(90, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(3, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string Location { get; set; }
        [DisplayName("Başlama Tarixi")]
        [Required(ErrorMessage = "{0} adı boş ola bilməz!")]
        [MaxLength(40, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(3, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string StartDate { get; set; }
        [DisplayName("Bitmə Tarixi")]
        [Required(ErrorMessage = "{0} adı boş ola bilməz!")]
        [MaxLength(40, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(3, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string EndDate { get; set; }
        [DisplayName("Qiymət")]
        [Required(ErrorMessage = "{0} adı boş ola bilməz!")]
        [MaxLength(20, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(1, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string Price { get; set; }
        [DisplayName("İnformasiya")]
        [Required(ErrorMessage = "{0} adı boş ola bilməz!")]
        [MinLength(20, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string Info { get; set; }
        [DisplayName("Mətn")]
        [Required(ErrorMessage = "{0} adı boş ola bilməz!")]
        [MinLength(20, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string Description { get; set; }
        [DisplayName("Şəkil")]
        [Required(ErrorMessage = "{0} sahəsi boş ola bilməz")]
        [MinLength(5, ErrorMessage = "{0} sahəsi {1} dən kiçik ola bilməz")]
        public string Photo { get; set; }
        [DisplayName("Aktivdir ?")]
        [Required(ErrorMessage = "{0}  boş ola bilməz!")]
        public bool IsActive { get; set; }
        [DisplayName("Silinib mi ?")]
        [Required(ErrorMessage = "{0}  boş ola bilməz!")]
        public bool IsDeleted { get; set; }

    }
}
