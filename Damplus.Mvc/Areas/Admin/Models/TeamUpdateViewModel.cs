using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Models
{
    public class TeamUpdateViewModel
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("AdSoyad")]
        [MaxLength(60, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(3, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string Fullname { get; set; }
        [DisplayName("Pozisiya")]
        [MaxLength(30, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(3, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string Position { get; set; }
        [DisplayName("Şəkil")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }
        public string Photo { get; set; }
        [DisplayName("Aktivdir ?")]
        public bool IsActive { get; set; }
        [DisplayName("Silinib mi ?")]
        public bool IsDeleted { get; set; }
    }
}
