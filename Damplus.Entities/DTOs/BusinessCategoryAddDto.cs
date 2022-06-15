using Microsoft.AspNetCore.Http;
using Damplus.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.DTOs
{
    public class BusinessCategoryAddDto:DtoGetBase
    {
        [DisplayName("Layihe Kateqoriya Adı")]
        [Required(ErrorMessage = "{0} adı boş ola bilməz!")] //{O} = Kateqoriya Adı
        [MaxLength(70, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")] //{1} = 70
        [MinLength(3, ErrorMessage = "{0} {1} - dən az ola bilməz!")] //{1} = 3
        public string Name { get; set; }
        [DisplayName("Kateqoriya Açıqlaması")]
        [MaxLength(500, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(3, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string Description { get; set; }
        [DisplayName("Şəkil")]
        [Required(ErrorMessage = "Zəhmət olmasa {0} seçin")]
        public string Icon { get; set; }
        [DisplayName("Parent Kateqoriya")]
        public int UpperCategoryId { get; set; }

        [DisplayName("Aktivdir ?")]
        [Required(ErrorMessage = "{0}  boş ola bilməz!")]
        public bool IsActive { get; set; }
    }
}
