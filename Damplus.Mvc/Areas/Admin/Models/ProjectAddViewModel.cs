﻿using Damplus.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Models
{
    public class ProjectAddViewModel
    {
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
        [DisplayName("Tarix")]
        [Required(ErrorMessage = "{0} sahəsi boş ola bilməz")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
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
        public IFormFile Photo { get; set; }
        [DisplayName("Aktivdir ?")]
        [Required(ErrorMessage = "{0} boş ola bilməz!")]
        public bool IsActive { get; set; }
        [DisplayName("Silinib ?")]
        [Required(ErrorMessage = "{0} boş ola bilməz!")]
        public bool IsDeleted { get; set; }
        [DisplayName("Kateqoriya")]
        [Required(ErrorMessage = "{0} sahəsi boş ola bilməz")]
        public int ProjectCategoryId { get; set; }
        public IList<ProjectCategory> ProjectCategories { get; set; }
        public IFormFileCollection ProjectPhotos { get; set; }
        public IList<PhotoAddViewModel> Photos { get; set; }
    }
}
