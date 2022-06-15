using Microsoft.AspNetCore.Http;
using Damplus.Shared.Entities.Abstract;
using Damplus.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.DTOs
{
    public class UserUpdateDto:DtoGetBase
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("İstifadəçi Adı")]
        [Required(ErrorMessage = "{0} boş olmamalıdır")]
        [MaxLength(50, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(3, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string UserName { get; set; }
        [DisplayName("Email Ünvanı")]
        [Required(ErrorMessage = "{0} boş olmamalıdır")]
        [MaxLength(100, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(10, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [DisplayName("Telefon Nömrəsi")]
        [Required(ErrorMessage = "{0} boş olmamalıdır")]
        [MaxLength(13, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")] // +994503653319 
        [MinLength(13, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [DisplayName("Şəkil")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }
        [DisplayName("Şəkil")]
        public string Picture { get; set; }
        [DisplayName("Adı")]
        [MaxLength(30, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(2, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string FirstName { get; set; }
        [DisplayName("Soyadı")]
        [MaxLength(30, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(2, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string LastName { get; set; }
        [DisplayName("Haqqında")]
        [MaxLength(1000, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(5, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string About { get; set; }
        [DisplayName("Twitter")]
        [MaxLength(250, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(20, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string TwitterLink { get; set; }
        [DisplayName("Facebook")]
        [MaxLength(250, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(20, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string FacebookLink { get; set; }
        [DisplayName("Instagram")]
        [MaxLength(250, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(20, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string InstagramLink { get; set; }
        [DisplayName("LinkedIn")]
        [MaxLength(250, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(20, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string LinkedInLink { get; set; }
        [DisplayName("Youtube")]
        [MaxLength(250, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(20, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string YoutubeLink { get; set; }
        [DisplayName("GitHub")]
        [MaxLength(250, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(20, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string GitHubLink { get; set; }
        [DisplayName("Website")]
        [MaxLength(250, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(20, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string WebsiteLink { get; set; }
    }
}
