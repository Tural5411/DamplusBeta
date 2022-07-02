using Damplus.Shared.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Damplus.Entities.Concrete;

namespace Damplus.Entities.DTOs
{
    public class BusinessAddDto
    {
        [DisplayName("Başlıq")]
        [Required(ErrorMessage = "{0} sahəsi boş ola bilməz")]
        [MaxLength(100, ErrorMessage = "{0} sahəsi {1} dən böyük ola bilməz")]
        [MinLength(5, ErrorMessage = "{0} sahəsi {1} dən kiçik ola bilməz")]
        public string Title { get; set; }
        [DisplayName("Mətn")]
        [Required(ErrorMessage = "{0} sahəsi boş ola bilməz")]
        [MinLength(20, ErrorMessage = "{0} sahəsi {1} dən kiçik ola bilməz")]
        public string Content { get; set; }
        [DisplayName("Şəkil")]
        [Required(ErrorMessage = "{0} sahəsi boş ola bilməz")]
        [MaxLength(300, ErrorMessage = "{0} sahəsi {1} dən böyük ola bilməz")]
        [MinLength(5, ErrorMessage = "{0} sahəsi {1} dən kiçik ola bilməz")]
        public IFormFile PictureFile { get; set; }
        public string Thumbnail { get; set; }
        [DisplayName("Tarix")]
        [Required(ErrorMessage = "{0} sahəsi boş ola bilməz")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        [DisplayName("Seo müəllif")]
        [Required(ErrorMessage = "{0} sahəsi boş ola bilməz")]
        [MaxLength(60, ErrorMessage = "{0} sahəsi {1} dən böyük ola bilməz")]
        [MinLength(1, ErrorMessage = "{0} sahəsi {1} dən kiçik ola bilməz")]
        public string SeoAuthor { get; set; }
        [DisplayName("Seo açıqlama")]
        [Required(ErrorMessage = "{0} sahəsi boş ola bilməz")]
        [MaxLength(150, ErrorMessage = "{0} sahəsi {1} dən böyük ola bilməz")]
        [MinLength(1, ErrorMessage = "{0} sahəsi {1} dən kiçik ola bilməz")]
        public string SeoDescription { get; set; }
        [DisplayName("Seo etiket")]
        [Required(ErrorMessage = "{0} sahəsi boş ola bilməz")]
        [MaxLength(150, ErrorMessage = "{0} sahəsi {1} dən böyük ola bilməz")]
        [MinLength(1, ErrorMessage = "{0} sahəsi {1} dən kiçik ola bilməz")]
        public string SeoTags { get; set; }
        
        [DisplayName("Aktivdir ?")]
        [Required(ErrorMessage = "{0} boş ola bilməz!")]
        public bool IsActive { get; set; }
        [DisplayName("Silinib ?")]
        [Required(ErrorMessage = "{0} boş ola bilməz!")]
        public bool IsDeleted { get; set; }
    }
}
